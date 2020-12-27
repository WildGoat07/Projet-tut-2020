<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$horscomp = new stdClass();
$horscomp->values = [];
$horscomp->success = true;
$horscomp->errors = [];

$strReq = "SELECT `id_ens`, `id_comp`, `annee`, `HCM`, `HEI`, `HTD`, `HTP`, `HTPL`, `HPRJ`, `HEqTD` FROM `horscomp`";
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
        foreach ($postObj->filters->id_ens as $id_ens) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`id_ens` = \"$id_ens\"";
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
    if (isset($postObj->filters->annee)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->annee as $annee) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`annee` = \"$annee\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->HCM)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HCM->min)) {
            $strReq .= "`HCM` >= " . $postObj->filters->HCM->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HCM->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HCM` <= " . $postObj->filters->HCM->max;
        }
    }
    if (isset($postObj->filters->HEI)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HEI->min)) {
            $strReq .= "`HEI` >= " . $postObj->filters->HEI->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HEI->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HEI` <= " . $postObj->filters->HEI->max;
        }
    }
    if (isset($postObj->filters->HTD)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HTD->min)) {
            $strReq .= "`HTD` >= " . $postObj->filters->HTD->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HTD->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HTD` <= " . $postObj->filters->HTD->max;
        }
    }
    if (isset($postObj->filters->HTP)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HTP->min)) {
            $strReq .= "`HTP` >= " . $postObj->filters->HTP->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HTP->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HTP` <= " . $postObj->filters->HTP->max;
        }
    }
    if (isset($postObj->filters->HTPL)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HTPL->min)) {
            $strReq .= "`HTPL` >= " . $postObj->filters->HTPL->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HTPL->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HTPL` <= " . $postObj->filters->HTPL->max;
        }
    }
    if (isset($postObj->filters->HPRJ)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HPRJ->min)) {
            $strReq .= "`HPRJ` >= " . $postObj->filters->HPRJ->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HPRJ->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HPRJ` <= " . $postObj->filters->HPRJ->max;
        }
    }
    if (isset($postObj->filters->HEqTD)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HEqTD->min)) {
            $strReq .= "`HEqTD` >= " . $postObj->filters->HEqTD->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HEqTD->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HEqTD` <= " . $postObj->filters->HEqTD->max;
        }
    }
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
    if ($requete->rowCount() != 0)) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->id_ens = utf8_encode($req['id_ens']);
        
            $obj->id_comp = utf8_encode($req['id_comp']);
        
            $obj->annee = utf8_encode($req['annee']);
        
            $obj->HCM = utf8_encode($req['HCM']);
        
            $obj->HEI = utf8_encode($req['HEI']);
        
            $obj->HTD = utf8_encode($req['HTD']);
        
            $obj->HTP = utf8_encode($req['HTP']);
        
            $obj->HTPL = utf8_encode($req['HTPL']);
        
            $obj->HPRJ = utf8_encode($req['HPRJ']);
        
            $obj->HEqTD = utf8_encode($req['HEqTD']);
        
            $horscomp->values[] = $obj;
        }
    }
}
else {
    $horscomp->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $horscomp->errors[] = $obj;
}

echo json_encode($horscomp);