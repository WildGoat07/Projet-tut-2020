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
    $strReq = "INSERT INTO `annee_univ` (";
    $data = "(";

    $strReq .= " `annee` ";
    $data .= "'$values->annee'";
    array_push($id_entered, $values->annee);

    $strReq .= ") VALUES $data )";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();
    
    if ( $error[0] == '00000' ) {
        $nbRows = $createReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `annee` FROM `annee_univ` WHERE ";
            $resultStr .= "annee = $id_entered[$indexId]";
            $indexId++;

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->annee = $row->annee;

            array_push($returnedValues->values, $obj);
            $returnedValues->success=true;
        } else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            $returnedValues->errors[] = $obj;
        }
    }
    else {
        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreru renvoyé
        array_push($returnedValues->errors, $obj);
    }
}

echo json_encode($returnedValues);