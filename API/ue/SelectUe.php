<?php
require_once '../app/Database.php';
require_once '../utilities.php';

header('Content-Type: application/json');

$ue = new stdClass();
$ue->values = [];
$ue->success = true;
$ue->errors = [];

$strReq = "SELECT `code_ue`, `libelle_ue`, `nature`, `ECTS`, `code_ue_pere`, `code_sem` FROM `ue` ";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters)) {
    $firstFilter = true;
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
            if( trim($code_ue_pere) === "" )
                $strReq .= "`code_ue_pere` IS NULL";
            else
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
            if( trim($code_sem) === "" )
                $strReq .= "`code_sem` IS NULL";
            else
                $strReq .= "`code_sem` = \"$code_sem\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
}

if (isset($postObj->search)) {
    $strReq .= $whereSet?" AND ":" WHERE ";

    $search = cleanString($postObj->search);

    $strReq .= " compareStrings(\"$search\", `libelle_ue`) ";

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
    if ($requete->rowCount() != 0) {
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
    }
}
else {
    $ue->success = false;
    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $ue->errors[] = $obj;
}

echo json_encode($ue);