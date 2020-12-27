<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success=true;
$returnedValues->errors=[];

foreach ($postObj->values as $values) {
    $id_entered = $values->annee;

    $strReq = "INSERT INTO `annee_univ` (`annee`) VALUES ('$values->annee')";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();
    
    if ( $error[0] == '00000' ) {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `annee` FROM `annee_univ` WHERE ";
            $resultStr .= " `annee`='$id_entered' ";

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->annee = $row->annee;

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
        $obj->error_desc = $error[2]; //enregistrement message d'erreru renvoyé
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);