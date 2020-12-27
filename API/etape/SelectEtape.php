<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$etape = new stdClass();
$etape->values = [];
$etape->success = true;
$etape->errors = [];

$strReq = "SELECT `code_etape`, `vet`, `libelle_vet`, `id_comp`, `code_diplome`, `vdi` FROM `etape`";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters)) {
    $firstFilter = true;
    if (isset($postObj->filters->code_etape)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_etape as $code_etape) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`code_etape` = \"$code_etape\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->vet)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->vet as $vet) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`vet` = \"$vet\"";
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
            $strReq .= "`id_comp` = \"$id_comp\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->code_diplome)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_diplome as $code_diplome) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            if( trim($code_diplome) === "" )
                $strReq .= "`code_diplome` IS NULL";
            else
                $strReq .= "`code_diplome` = \"$code_diplome\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->vdi)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->vdi as $vdi) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            if( trim($vdi) === "" )
                $strReq .= "`vdi` IS NULL";
            else
                $strReq .= "`vdi` = \"$vdi\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
}

if (isset($postObj->search)) {
    $strReq .= $whereSet?" AND ":" WHERE ";

    $search = cleanString($postObj->search);

    $strReq .= " compareStrings(\"$search\", `libelle_vet`) ";

}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `code_etape` DESC, `vet` DESC ";
else
    $strReq .= " ORDER BY `code_etape`, `vet` DESC ";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    if ($requete->rowCount() != 0)) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->code_etape = utf8_encode($req['code_etape']);
        
            $obj->vet = utf8_encode($req['vet']);
        
            $obj->libelle_vet = utf8_encode($req['libelle_vet']);
        
            $obj->id_comp = utf8_encode($req['id_comp']);
        
            $obj->code_diplome = utf8_encode($req['code_diplome']);
        
            $obj->vdi = utf8_encode($req['vdi']);
        
            $etape->values[] = $obj;
        }
    }
}
else {
    $etape->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $etape->errors[] = $obj;
}

echo json_encode($etape);