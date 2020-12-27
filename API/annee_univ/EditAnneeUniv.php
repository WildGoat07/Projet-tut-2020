<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success=true;
$returnedValues->errors=[];

foreach ($postObj->values as $values) {
    $strReq = "UPDATE `annee_univ` SET ";

    $data = $values->data;

    if( isset($data->annee) ) 
        $strReq .= "`annee` = '$data->annee'";

    $target = $values->target;
    $strReq .= " WHERE `annee` = '$target->annee' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        if ($updateReq->rowCount() != 0) {
            $resultStr = "SELECT `annee` FROM `annee_univ` WHERE ";
            if( isset($data->annee) )
                $resultStr .= " `annee` = '$data->annee' ";
            else 
                $resultStr .= " `annee` = '$target->annee' ";

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
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);