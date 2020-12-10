<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
    $returnedValues->values = [];
    $returnedValues->success=false;
    $returnedValues->errors=[];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `ue` SET ";

    $data = $values->data;

    if( isset($data->code_ue) ) 
        $strReq .= "`code_ue` = '$data->code_ue'";
    if( isset($data->libelle_ue) )
        $strReq .= ",`libelle_ue` = '$data->libelle_ue'";
    if( isset($data->nature) )
        $strReq .= ",`nature` = '$data->nature'";
    if( isset($data->ECTS) )
        $strReq .= ",`ECTS` = '$data->codECTSe_ue'";
    if( isset($data->code_ue_pere) )
        $strReq .= ",`code_ue_pere` = '$data->code_ue_pere'";
    if( isset($data->code_sem) )
        $strReq .= ",`code_sem` = '$data->code_sem'";

    $target = $values->target;
    $strReq .= " WHERE `code_ue` = '$target->code_ue' ";


    $updateReq=$db->prepare($strReq);
    if ( $updateReq->execute() ) {
        $nbRows=$updateReq->rowCount();
        if( $nbRows != 0) {
            $resultStr = "SELECT `code_ue`, `libelle_ue`, `nature`, `ECTS`, `code_ue_pere`, `code_sem` FROM `ue` WHERE ";
            $resultStr .= "`code_ue` = '$data->code_ue'";

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->code_ue = $row->code_ue;
            $obj->libelle_ue = $row->libelle_ue;
            $obj->nature = $row->nature;
            $obj->ECTS = $row->ECTS;
            $obj->code_ue_pere = $row->code_ue_pere;
            $obj->code_sem = $row->code_sem;

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