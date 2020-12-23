<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success=true;
$returnedValues->errors=[];


foreach ($postObj->values as $values) {
    $id_entered = $values->no_cat;

    $strReq = "INSERT INTO `categories` (`no_cat`,`categorie`) VALUES ('$values->no_cat','$values->categorie')";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();
    
    if ( $error[0] == '00000' ) {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `no_cat`,`categorie` FROM `categories` WHERE ";
            $resultStr .= " `no_cat`='$id_entered' ";

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->no_cat = $row->no_cat;
            $obj->categorie = $row->categorie;

            $returnedValues->values[] = $obj;
        }
        else {
            $returnedValues->success=false;
            
            $obj = new stdClass();
            $obj->error_code = '66666'; //enregistrement code d'erreur
            $obj->error_desc = '0 rows affected'; //enregistrement message d'erreru renvoyé
            $returnedValues->errors[] = $obj;
        }
    }   
    else {
        $returnedValues->success=false;

        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreur renvoyé
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);