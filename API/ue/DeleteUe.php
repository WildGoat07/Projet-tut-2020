<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$suppressedValues = new stdClass;
$suppressedValues->success=true;
$suppressedValues->errors=[];

foreach ($postObj->values as $values) {
    $strReq = "DELETE FROM `ue` WHERE `code_ue`= '$values->code_ue'";

    $deleteReq = $db->prepare($strReq);
    $statement = $deleteReq->execute();
    $error = $deleteReq->errorInfo();

    if( $error[0] == '00000' ) {
        if( $deleteReq->rowCount() == 0 ) {
            $suppressedValues->success=false;
        
            $obj = new stdClass();
            $obj->error_code = '66666'; //enregistrement code d'erreur
            $obj->error_desc = '0 rows affected'; //enregistrement message d'erreru renvoyé
            $suppressedValues->errors[] = $obj;
        }
    }
    else {
        $suppressedValues->success=false;
        
        $obj = new stdClass();
        $obj->error_code = $error[0]; //enregistrement code d'erreur
        $obj->error_desc = $error[2]; //enregistrement message d'erreru renvoyé
        $suppressedValues->errors[] = $obj;
    }
}

echo json_encode($suppressedValues);