<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $firstValue = true;

    $strReq = "UPDATE `horscomp` SET ";

    $data = $values->data;

    if( isset($data->id_ens) ) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`id_ens` = '$data->id_ens'";
    }
    if( isset($data->id_comp) ) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`id_comp` = '$data->id_comp'"; 
    }
    if( isset($data->annee) ) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`annee` = '$data->annee'";
    }
    if( isset($data->HCM) ) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HCM` = '$data->HCM'"; 
    }
    if( isset($data->HEI) ) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HEI` = '$data->HEI'";
    }
    if( isset($data->HTD) ) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HTD` = '$data->HTD'";
    }
    if( isset($data->HTP) )  {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HTP` = '$data->HTP'";
    }
    if( isset($data->HTPL) )  {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HTPL` = '$data->HTPL'";
    }
    if( isset($data->HPRJ) ) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HPRJ` = '$data->HPRJ'";
    }
    if( isset($data->HEqTD) ) {
        if (!$firstValue)
            $strReq .= ",";
        $firstValue = false;
        $strReq .= "`HEqTD` = '$data->HEqTD'";   
    }

    $target = $values->target;
    $strReq .= " WHERE `id_ens`='$target->id_ens' AND `id_comp`='$target->id_comp' AND `annee`='$target->annee' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') { 
        if ($updateReq->rowCount() != 0) {
            $resultStr = "SELECT `id_ens`, `id_comp`, `annee`, `HCM`, `HEI`, `HTD`, `HTP`, `HTPL`, `HPRJ`, `HEqTD` FROM `horscomp` WHERE ";
            
            $firstValue=true;
            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if( isset($data->id_ens) )
                $resultStr .= "(`id_ens` = '$data->id_ens')";
            else 
                $resultStr .= "(`id_ens` = '$target->id_ens')";
                
            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if( isset($data->id_comp) )
                $resultStr .= "(`id_comp` = '$data->id_comp')";
            else 
                $resultStr .= "(`id_comp` = '$target->id_comp')";

            if (!$firstValue)
                $resultStr .= " AND ";
            $firstValue = false;
            if( isset($data->annee) )
                $resultStr .= "(`annee` = '$data->annee')";
            else 
                $resultStr .= "(`annee` = '$target->annee')";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->id_ens = $row->id_ens;
            $obj->id_comp = $row->id_comp;
            $obj->annee = $row->annee;
            $obj->HCM = $row->HCM;
            $obj->HEI = $row->HEI;
            $obj->HTD = $row->HTD;
            $obj->HTP = $row->HTP;
            $obj->HTPL = $row->HTPL;
            $obj->HPRJ = $row->HPRJ;
            $obj->HeqTD = $row->HEqTD;

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