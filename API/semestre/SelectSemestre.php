<?php
require_once '../app/Database.php';
require_once '../utilities.php';

header('Content-Type: application/json');

$semestre = new stdClass();
$semestre->values = [];
$semestre->success = true;
$semestre->errors = [];

$strReq = "SELECT `code_sem`, `libelle_sem`, `no_sem`, `code_etape`, `vet` FROM `semestre`";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters)) {
    $firstFilter = true;
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
    if (isset($postObj->filters->no_sem)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->no_sem as $no_sem) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            if (trim($no_sem) === "")
                $strReq .= "`no_sem` IS NULL";
            else
                $strReq .= "`no_sem` = \"$no_sem\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
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
            if (trim($code_etape) === "")
                $strReq .= "`code_etape` IS NULL";
            else
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
            if (trim($vet) === "")
                $strReq .= "`vet` IS NULL";
            else
                $strReq .= "`vet` = \"$vet\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
}

if (isset($postObj->search)) {
    $strReq .= $whereSet ? " AND " : " WHERE ";

    $search = cleanString($postObj->search);

    $strReq .= " compareStrings(\"$search\", `libelle_sem`) ";
}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `code_sem` DESC ";
else
    $strReq .= " ORDER BY `code_sem`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0] == '00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();

            $obj->code_sem = utf8_encode($req['code_sem']);

            $obj->libelle_sem = utf8_encode($req['libelle_sem']);

            $obj->no_sem = utf8_encode($req['no_sem']);

            $obj->code_etape = utf8_encode($req['code_etape']);

            $obj->vet = utf8_encode($req['vet']);

            $semestre->values[] = $obj;
        }
    }
} else {
    $semestre->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $semestre->errors[] = $obj;
}

echo json_encode($semestre);
