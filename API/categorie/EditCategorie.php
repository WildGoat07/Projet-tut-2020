<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
    $returnedValues->values = [];
    $returnedValues->success=false;
    $returnedValues->errors=[];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `categories` SET ";

    $data = $values->data;

    if( isset($data->no_cat) ) 
        $strReq .= "`no_cat` = '$data->no_cat'";
    if( isset($data->categorie) ) 
        $strReq .= "`categorie` = '$data->categorie'";

    $target = $values->target;
    $strReq .= " WHERE `no_cat` = '$target->no_cat' ";
    
    var_dump($strReq);

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ( $error[0] == '00000' ) {
        $nbRows=$updateReq->rowCount();
        if( $nbRows != 0) {
            $resultStr = "SELECT `no_cat`, `categorie` FROM `categories` WHERE ";
            if( isset($data->no_cat) ) 
                $resultStr .= "`no_cat` = '$data->no_cat'";
            else 
                $resultStr .= "`no_cat` = '$target->no_cat'";

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->no_cat = $row->no_cat;
            $obj->categorie = $row->categorie;

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
        $error=$updateReq->errorInfo();

        $obj = new stdClass();
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);