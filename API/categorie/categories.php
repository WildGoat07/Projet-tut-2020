<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$strReq = "SELECT `no_cat`, `categorie` FROM `categories`";
$postObj = json_decode(file_get_contents('php://input'));
if (isset($postObj->filters)) {
    $firstFilter = true;
    $whereSet = false;
    if (isset($postObj->filters->no_cat)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->no_cat as $no_cat) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`no_cat` = \"$no_cat\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->categorie)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->categorie as $categorie) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`categorie` = \"$categorie\"";
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
    $strReq .= " ORDER BY DESC `no_cat`";
else
    $strReq .= " ORDER BY `no_cat`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->query($strReq);

$categorie = new stdClass();
$categorie->values = [];

foreach ($requete as $req) {
    $obj = new stdClass();

    $obj->no_cat = utf8_encode($req['no_cat']);

    $obj->categorie = utf8_encode($req['categorie']);

    $categorie->values[] = $obj;
}

$categorie->success = true;

echo json_encode($categorie);
