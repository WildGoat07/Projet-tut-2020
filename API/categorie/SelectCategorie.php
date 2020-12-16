<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$categories = new stdClass();
$categories->values = [];
$categories->success = false;
$categories->errors = [];

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
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `no_cat` DESC ";
else
    $strReq .= " ORDER BY `no_cat`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    foreach ($requete as $req) {
        $obj = new stdClass();
    
        $obj->no_cat = utf8_encode($req['no_cat']);
    
        $obj->categorie = utf8_encode($req['categorie']);
    
        $categories->values[] = $obj;
    }

    $categories->success = true;
}
else {
    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $categories->errors[] = $obj;
}


echo json_encode($categories);
