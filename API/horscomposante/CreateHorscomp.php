<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $id_entered_ens = $values->id_ens;
    $id_entered_comp = $values->id_comp;
    $id_entered_annee = $values->annee;

    $strReq = "INSERT INTO `horscomp` (";
    $data = "(";

    $strReq .= " `id_ens` ";
    $data .= "'$values->id_ens'";

    $strReq .= " ,`id_comp` ";
    $data .= ",'$values->id_comp'";

    $strReq .= " ,`annee` ";
    $data .= ",'$values->annee'";

    if (isset($values->HCM)) {
        $strReq .= " ,`HCM` ";
        $data .= ",'$values->HCM'";
    }

    if (isset($values->HEI)) {
        $strReq .= " ,`HEI` ";
        $data .= ",'$values->HEI'";
    }

    if (isset($values->HTD)) {
        $strReq .= " ,`HTD` ";
        $data .= ",'$values->HTD'";
    }

    if (isset($values->HTP)) {
        $strReq .= " ,`HTP` ";
        $data .= ",'$values->HTP'";
    }

    if (isset($values->HTPL)) {
        $strReq .= " ,`HTPL` ";
        $data .= ",'$values->HTPL'";
    }

    if (isset($values->HPRJ)) {
        $strReq .= " ,`HPRJ` ";
        $data .= ",'$values->HPRJ'";
    }

    if (isset($values->HEqTD)) {
        $strReq .= " ,`HEqTD` ";
        $data .= ",'$values->HEqTD'";
    }

    $strReq .= ") VALUES $data )";

    $createReq = $db->prepare($strReq);
    $statement = $createReq->execute();
    $error = $createReq->errorInfo();
    
    if ($error[0] == '00000') {
        if ($createReq->rowCount() != 0) {
            $resultStr = "SELECT `id_ens`, `id_comp`, `annee`, `HCM`, `HEI`, `HTD`, `HTP`, `HTPL`, `HPRJ`, `HEqTD` FROM `horscomp` WHERE ";
            $resultStr .= "`id_ens`='$id_entered_ens' AND `id_comp`='$id_entered_comp' AND `annee`='$id_entered_annee'";

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
            $obj->HEqTD = $row->HEqTD;

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