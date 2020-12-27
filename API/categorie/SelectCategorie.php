<?php
require_once '../app/Database.php';
require_once '../utilities.php';

header('Content-Type: application/json');

$categories = new stdClass();
$categories->values = [];
$categories->success = true;
$categories->errors = [];

$strReq = "SELECT `no_cat`, `categorie` FROM `categories` ";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters))
    if (isset($postObj->filters->no_cat)) {
        $whereSet = true;
        $firstArrayFilter=true;
        
        $strReq .= ' WHERE ( ';
        foreach ($postObj->filters->no_cat as $no_cat) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            
            $strReq .= " `no_cat` = \"$no_cat\" ";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }

if (isset($postObj->search)) {
    $strReq .= $whereSet?" AND ":" WHERE ";

    $search = cleanString($postObj->search);

    $strReq .= " compareStrings(\"$search\", `categorie`) ";
}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `no_cat` DESC ";
else
    $strReq .= " ORDER BY `no_cat` ";
$strReq .= " LIMIT $postObj->quantity OFFSET $postObj->skip ";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->no_cat = utf8_encode($req['no_cat']);
            $obj->categorie = utf8_encode($req['categorie']);
        
            $categories->values[] = $obj;
        }
    }
}
else {
    $categories->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $categories->errors[] = $obj;
}

echo json_encode($categories);
