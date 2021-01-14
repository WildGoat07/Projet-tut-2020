<?php
require_once '../app/Database.php';
require_once '../utilities.php';

header('Content-Type: application/json');

$composante = new stdClass();
$composante->values = [];
$composante->success = true;
$composante->errors = [];

$strReq = "SELECT `id_comp`, `nom_comp`, `lieu_comp` FROM `composante`";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters))
    if (isset($postObj->filters->id_comp)) {
        $whereSet = true;
        $firstArrayFilter=true;
        
        $strReq .= ' WHERE ( ';
        foreach ($postObj->filters->id_comp as $id_comp) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            
            $strReq .= " `id_comp` = \"$id_comp\" ";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }

if (isset($postObj->search)) {
    $strReq .= $whereSet?" AND ":" WHERE ";

    $search = cleanString($postObj->search);

    $strReq .= " (compareStrings(\"$search\", `nom_comp`) OR compareStrings(\"$search\", `lieu_comp`))";
}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `id_comp` DESC ";
else
    $strReq .= " ORDER BY `id_comp`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0]=='00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();
        
            $obj->id_comp = $req['id_comp'];
        
            $obj->nom_comp = $req['nom_comp'];
        
            $req['lieu_comp'] == null ? null : $obj->lieu_comp = $req['lieu_comp'];
        
            $composante->values[] = $obj;
        }
    }
}
else {
    $composante->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $composante->errors[] = $obj;
}

echo json_encode($composante);