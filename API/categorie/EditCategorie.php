<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success=true;
$returnedValues->errors=[];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `categories` SET ";

    $data = $values->data;

    $firstData = true;

    if( isset($data->no_cat) ) {
        $strReq .= " `no_cat` = '$data->no_cat' ";
        $firstData = false;
    }
    if( isset($data->categorie) ) {
        if( !$firstData )
            $strReq .= " , ";
        $strReq .= " `categorie` = '$data->categorie' ";
    }
        
    $target = $values->target;
    $strReq .= " WHERE `no_cat` = '$target->no_cat' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ( $error[0] == '00000' ) {
        if( $updateReq->rowCount() != 0) {
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