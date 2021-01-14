<?php
require_once '../app/Database.php';
require_once '../utilities.php';

header('Content-Type: application/json');

$service = new stdClass();
$service->values = [];
$service->success = true;
$service->errors = [];

$strReq = "SELECT `id_ens`, `code_ec`, `annee`, `NbGpCM`, `NbGpEI`, `NbGpTD`, `NbGpTP`, `NbGpTPL`, `NbGpPRJ`, `HEqTD` FROM `service` ";
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
    if (isset($postObj->filters->code_ec)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_ec as $code_ec) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`code_ec` = \"$code_ec\"";
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
    if (isset($postObj->filters->NbGpCM)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->NbGpCM->min)) {
            $strReq .= "`NbGpCM` >= " . $postObj->filters->NbGpCM->min;
            $minSet = true;
        }
        if (isset($postObj->filters->NbGpCM->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`NbGpCM` <= " . $postObj->filters->NbGpCM->max;
        }
    }
    if (isset($postObj->filters->NbGpEI)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->NbGpEI->min)) {
            $strReq .= "`NbGpEI` >= " . $postObj->filters->NbGpEI->min;
            $minSet = true;
        }
        if (isset($postObj->filters->NbGpEI->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`NbGpEI` <= " . $postObj->filters->NbGpEI->max;
        }
    }
    if (isset($postObj->filters->NbGpTD)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->NbGpTD->min)) {
            $strReq .= "`NbGpTD` >= " . $postObj->filters->NbGpTD->min;
            $minSet = true;
        }
        if (isset($postObj->filters->NbGpTD->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`NbGpTD` <= " . $postObj->filters->NbGpTD->max;
        }
    }
    if (isset($postObj->filters->NbGpTP)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->NbGpTP->min)) {
            $strReq .= "`NbGpTP` >= " . $postObj->filters->NbGpTP->min;
            $minSet = true;
        }
        if (isset($postObj->filters->NbGpTP->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`NbGpTP` <= " . $postObj->filters->NbGpTP->max;
        }
    }
    if (isset($postObj->filters->NbGpTPL)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->NbGpTPL->min)) {
            $strReq .= "`NbGpTPL` >= " . $postObj->filters->NbGpTPL->min;
            $minSet = true;
        }
        if (isset($postObj->filters->NbGpTPL->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`NbGpTPL` <= " . $postObj->filters->NbGpTPL->max;
        }
    }
    if (isset($postObj->filters->NbGpPRJ)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->NbGpPRJ->min)) {
            $strReq .= "`NbGpPRJ` >= " . $postObj->filters->NbGpPRJ->min;
            $minSet = true;
        }
        if (isset($postObj->filters->NbGpPRJ->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`NbGpPRJ` <= " . $postObj->filters->NbGpPRJ->max;
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
    $strReq .= " ORDER BY `id_ens` DESC, `code_ec` DESC, `annee` DESC ";
else
    $strReq .= " ORDER BY `id_ens`, `code_ec`, `annee`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->id_ens = utf8_encode($req['id_ens']);
        
            $obj->code_ec = utf8_encode($req['code_ec']);
        
            $obj->annee = utf8_encode($req['annee']);
        
            $obj->NbGpCM = utf8_encode($req['NbGpCM']);
        
            $obj->NbGpEI = utf8_encode($req['NbGpEI']);
        
            $obj->NbGpTD = utf8_encode($req['NbGpTD']);
        
            $obj->NbGpTP = utf8_encode($req['NbGpTP']);
        
            $obj->NbGpTPL = utf8_encode($req['NbGpTPL']);
        
            $obj->NbGpPRJ = utf8_encode($req['NbGpPRJ']);
        
            $obj->NbHEqTDGp = utf8_encode($req['HEqTD']);
        
            $service->values[] = $obj;
        }
    }
}
else {
    $service->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $service->errors[] = $obj;
}

echo json_encode($service);