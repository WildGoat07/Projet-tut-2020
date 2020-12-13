<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
    $returnedValues->values = [];
    $returnedValues->success=false;
    $returnedValues->errors=[];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `horscomp` SET ";

    $data = $values->data;

    if( isset($data->id_ens) ) 
        $strReq .= "`id_ens` = '$data->id_ens'";

    if( isset($data->id_comp) )
        $strReq .= "`id_comp` = '$data->id_comp'"; 

    if( isset($data->annee) ) 
        $strReq .= "`annee` = '$data->annee'";

    if( isset($data->HCM) )
        $strReq .= "`HCM` = '$data->HCM'"; 

    if( isset($data->HEI) ) 
        $strReq .= "`HEI` = '$data->HEI'";

    if( isset($data->HTD) ) 
        $strReq .= "`HTD` = '$data->HTD'";

    if( isset($data->HTP) ) 
        $strReq .= "`HTP` = '$data->HTP'";

    if( isset($data->HTPL) ) 
        $strReq .= "`HTPL` = '$data->HTPL'";

    if( isset($data->HPRJ) ) 
        $strReq .= "`HPRJ` = '$data->HPRJ'";

    if( isset($data->HEqTD) ) 
        $strReq .= "`HEqTD` = '$data->HEqTD'";   
    

    $target = $values->target;
    $strReq .= " WHERE `id_ens` = '$target->id_ens' AND `id_comp` = '$target->id_comp' AND `annee`= '$target->annee' ";


    $updateReq=$db->prepare($strReq);
    if ( $updateReq->execute() ) {
        echo 'ok'; 
        $nbRows=$updateReq->rowCount();
        if( $nbRows != 0) {
            $resultStr = "SELECT `id_ens`, `ìd_comp`, `annee`, `HCM`, `HEI`, `HTD`, `HTP`, `HTPL`, `HPRJ`, `HEqTD` FROM `horscomp` WHERE ";
            $resultStr .= "`id_ens` = '$data->id_ens' AND `id_comp` = '$data->id_comp' AND `annee` = '$data->annee'";

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

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

            var_dump($obj->id_ens);

            $returnedValues->values[] = $obj;
            $returnedValues->success=true;
        }
        else {
            $obj = new stdClass();
            $obj->error_desc = "0 row affected";
            var_dump($obj->id_ens);
            $returnedValues->errors[] = $obj;
        }
    }
    else {
        echo 'pas ok'; 
        $error=$updateReq->errorInfo();

        $obj = new stdClass();
        
        $obj->error_code = $error[0];
        $obj->error_desc = $error[2];
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);