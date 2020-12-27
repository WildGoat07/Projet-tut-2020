<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success = true;
$returnedValues->errors = [];


foreach ($postObj->values as $values) {
    $strReq = "UPDATE `composante` SET ";

    $data = $values->data;
    $firstData = true;

    if (isset($data->id_comp)) {
        $strReq .= "`id_comp` = '$data->id_comp'";
        $firstData = false;
    }
    if (isset($data->nom_comp)) {
        if( !$firstData )
            $strReq .= " , ";
        $firstData = false;
        $strReq .= " `nom_comp` = '$data->nom_comp' ";
    }
    if (isset($data->lieu_comp)) {
        if( !$firstData )
            $strReq .= " , ";
        $firstData = false;
        $strReq .= " `lieu_comp` = '$data->lieu_comp' ";
    }

    $target = $values->target;
    $strReq .= " WHERE `id_comp` = '$target->id_comp' ";

    $updateReq = $db->prepare($strReq);
    $statement = $updateReq->execute();
    $error = $updateReq->errorInfo();

    if ($error[0] == '00000') {
        if ($updateReq->rowCount() != 0) {
            $resultStr = "SELECT `id_comp`, `nom_comp`, `lieu_comp` FROM `composante` WHERE ";
            if( isset($data->no_cat) )
                $resultStr .= " `id_comp` = '$data->id_comp' ";
            else 
                $resultStr .= " `id_comp` = '$target->id_comp' ";

            $result = $db->query($resultStr);
            $row = $result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->id_comp = $row->id_comp;
            $obj->nom_comp = $row->nom_comp;
            $obj->lieu_comp = $row->lieu_comp;

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
