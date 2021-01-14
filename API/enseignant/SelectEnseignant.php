<?php
require_once '../app/Database.php';
require_once '../utilities.php';

header('Content-Type: application/json');

$enseignant = new stdClass();
$enseignant->values = [];
$enseignant->success = true;
$enseignant->errors = [];

$strReq = "SELECT `id_ens`, `nom`, `prenom`, `fonction`, `HOblig`, `HMax`, `CRCT`, `PES_PEDR`, `id_comp` FROM `enseignant`";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters)) {
    $firstFilter = true;

    if (isset($postObj->filters->id_ens)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->id_ens as $id) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= " `id_ens` = \"$id\" ";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->fonction)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->fonction as $fonction) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            if( $fonction == null )
                $strReq .= "`fonction` IS NULL";
            else
                $strReq .= " `fonction` = \"$fonction\" ";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->CRCT)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->CRCT as $CRCT) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= " `CRCT` = \"$CRCT\" ";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->PES_PEDR)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->PES_PEDR as $PES_PEDR) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= " `PES_PEDR` = \"$PES_PEDR\" ";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->id_comp)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->id_comp as $id_comp) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            if( $id_comp == null )
                $strReq .= "`id_comp` IS NULL";
            else
                $strReq .= " `id_comp` = \"$id_comp\" ";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->HOblig)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        if( $HOblig == null )
            $strReq .= "`HOblig` IS NULL";
        else {
            $minSet = false;
            if (isset($postObj->filters->HOblig->min)) {
                $strReq .= " `HOblig` >= " . $postObj->filters->HOblig->min;
                $minSet = true;
            }
            if (isset($postObj->filters->HOblig->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= " `HOblig` <= " . $postObj->filters->HOblig->max;
            }
        }
    }
    if (isset($postObj->filters->HMax)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        if( $HMax == null )
            $strReq .= "`HMax` IS NULL";
        else {
            $minSet = false;
            if (isset($postObj->filters->HMax->min)) {
                $strReq .= " `HMax` >= " . $postObj->filters->HMax->min;
                $minSet = true;
            }
            if (isset($postObj->filters->HMax->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= " `HMax` <= " . $postObj->filters->HMax->max;
            }
        }
    }
}

if (isset($postObj->search)) {
    $strReq .= $whereSet?" AND ":" WHERE ";

    $search = cleanString($postObj->search);

    $strReq .= " (compareStrings(\"$search\", `nom`) OR compareStrings(\"$search\", `prenom`)) ";
}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `id_ens` DESC ";
else
    $strReq .= " ORDER BY `id_ens`";
    
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->id_ens = utf8_encode($req['id_ens']);
        
            $obj->nom = utf8_encode($req['nom']);
        
            $obj->prenom = utf8_encode($req['prenom']);
        
            $obj->fonction = $req['fonction'] == null ? null : utf8_encode($req['fonction']);
        
            $obj->HOblig = $req['HOblig'] == null ? null : utf8_encode($req['HOblig']);
        
            $obj->HMax = $req['HMax'] == null ? null : utf8_encode($req['HMax']);
        
            $obj->CRCT = utf8_encode($req['CRCT']);
        
            $obj->PES_PEDR = utf8_encode($req['PES_PEDR']);
        
            $obj->id_comp = $req['id_comp'] == null ? null : utf8_encode($req['id_comp']);
        
            $enseignant->values[] = $obj;
        }
    }
}
else {
    $enseignant->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $enseignant->errors[] = $obj;
}

echo json_encode($enseignant);