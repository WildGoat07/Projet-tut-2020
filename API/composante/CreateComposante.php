<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$id_entered=[];
$indexId=0;

$returnedValues = new stdClass;
    $returnedValues->values = [];
    $returnedValues->success=false;
    $returnedValues->errors=[];


foreach ($postObj->values as $values) {
    $strReq = "INSERT INTO `composante` (";
    $data = "(";

    $strReq .= " `id_comp` ";
    $data .= "'$values->id_comp'";
    array_push($id_entered, $values->code_ue);

    $strReq .= " ,`nom_comp` ";
    $data .= ",'$values->nom_comp'";

    $strReq .= " ,`lieu_comp` ";
    $data .= ",'$values->lieu_comp'";

    $strReq .= ") VALUES $data )";

    $requete=$db->prepare($strReq);
    
    if ( $requete->execute() ) {
        $resultStr = "SELECT `id_comp`, `nom_comp`, `lieu_comp` FROM `composante` WHERE ";
        $resultStr .= "id_comp = $id_entered[$indexId]";
        $indexId++;

        $result=$db->query($resultStr);
        $row=$result->fetch(PDO::FETCH_OBJ);

        $obj = new stdClass();
        $obj->id_comp = $row->id_comp;
        $obj->nom_comp = $row->nom_comp;
        $obj->lieu_comp = $row->lieu_comp;

        array_push($returnedValues->values, $obj);
        $returnedValues->success=true;
    }
    else {
        $error=$requete->errorInfo();

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreru renvoyé
        array_push($returnedValues->errors, $obj);
    }
}

echo json_encode($returnedValues);