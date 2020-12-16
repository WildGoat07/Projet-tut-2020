<?php
require_once '../app/Database.php';

header('Content-Type: application/json');


$ue = new stdClass();
$ue->values = [];
$ue->success = false;
$ue->errors = [];

$strReq = "SELECT `code_ue`, `libelle_ue`, `nature`, `ECTS`, `code_ue_pere`, `code_sem` FROM `ue` ";
$postObj = json_decode(file_get_contents('php://input'));
if (isset($postObj->filters)) {
    $firstFilter = true;
    $whereSet = false;
    if (isset($postObj->filters->code_ue)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_ue as $code_ue) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`code_ue` = \"$code_ue\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->libelle_ue)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->libelle_ue as $libelle_ue) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`libelle_ue` = \"$libelle_ue\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->nature)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->nature as $nature) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`nature` = \"$nature\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->ECTS)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->ECTS as $ECTS) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`ECTS` = \"$ECTS\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->code_ue_pere)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_ue_pere as $code_ue_pere) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`code_ue_pere` = \"$code_ue_pere\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->code_sem)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_sem as $code_sem) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`code_sem` = \"$code_sem\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
}
if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order` ";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `code_ue` DESC ";
else
    $strReq .= " ORDER BY `code_ue` ";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    foreach ($requete as $req) {
        $obj = new stdClass();
    
        $obj->code_ue = utf8_encode($req['code_ue']);
    
        $obj->libelle_ue = utf8_encode($req['libelle_ue']);
    
        $obj->nature = utf8_encode($req['nature']);
    
        $obj->ECTS = utf8_encode($req['ECTS']);
    
        $obj->code_ue_pere = utf8_encode($req['code_ue_pere']);
    
        $obj->code_sem = utf8_encode($req['code_sem']);
    
        $ue->values[] = $obj;
    }
    
    $ue->success = true;
}
else {
    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $ue->errors[] = $obj;
}

echo json_encode($ue);
