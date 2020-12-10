<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$strReq = "SELECT `id_comp`, `nom_comp`, `lieu_comp` FROM `composante`";
$postObj = json_decode(file_get_contents('php://input'));
if (isset($postObj->filters)) {
    $firstFilter = true;
    $whereSet = false;
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
    if (isset($postObj->filters->nom_comp)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->nom_comp as $nom_comp) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`nom_comp` = \"$nom_comp\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->lieu_comp)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->lieu_comp as $lieu_comp) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`lieu_comp` = \"$lieu_comp\"";
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
    $strReq .= " ORDER BY DESC `id_comp`";
else
    $strReq .= " ORDER BY `id_comp`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->query($strReq);

$composante = new stdClass();
$composante->values = [];

foreach ($requete as $req) {
    $obj = new stdClass();

    $obj->id_comp = utf8_encode($req['id_comp']);

    $obj->nom_comp = utf8_encode($req['nom_comp']);

    $obj->lieu_comp = utf8_encode($req['lieu_comp']);

    $composante->values[] = $obj;
}

$composante->success = true;

echo json_encode($composante);
