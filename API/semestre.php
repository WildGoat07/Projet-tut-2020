<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$strReq = "SELECT `code_sem`, `libelle_sem`, `no_sem`, `code_etape`, `vet` FROM `semestre`";
$postObj = json_decode(file_get_contents('php://input'));
if (isset($postObj->filters)) {
    $firstFilter = true;
    $whereSet = false;
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
    if (isset($postObj->filters->libelle_sem)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->libelle_sem as $libelle_sem) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`libelle_sem` = \"$libelle_sem\"";
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
}
if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY DESC `$postObj->order`";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY DESC `code_sem`";
else
    $strReq .= " ORDER BY `code_sem`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->query($strReq);

$semestre = new stdClass();
$semestre->values = [];

foreach ($requete as $req) {
    $obj = new stdClass();

    $obj->code_sem = utf8_encode($req['code_sem']);

    $obj->libelle_sem = utf8_encode($req['libelle_sem']);

    $obj->no_sem = utf8_encode($req['no_sem']);

    $obj->code_etape = utf8_encode($req['code_etape']);

    $obj->vet = utf8_encode($req['vet']);

    $semestre->values[] = $obj;
}

$semestre->success = true;

echo json_encode($semestre);

