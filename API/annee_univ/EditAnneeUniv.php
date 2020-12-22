<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
    $returnedValues->values = [];
    $returnedValues->success=false;
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
        $nbRows = $updateReq->rowCount();
        if ($nbRows != 0) {
            $resultStr = "SELECT `annee` FROM `annee_univ` WHERE ";
            $resultStr .= "`annee` = '$data->annee'";

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->annee = $row->annee;

            $returnedValues->values[] = $obj;
            $returnedValues->success=true;
        }
        else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            $returnedValues->errors[] = $obj;
        }
    }
    else {
        $obj = new stdClass();
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);