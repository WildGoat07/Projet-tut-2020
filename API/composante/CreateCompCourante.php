<?php
require_once '../app/Database.php';

header('Content-Type: application/json');

$postObj = json_decode(utf8_encode(file_get_contents('php://input')));

$returnedValues = new stdClass;
$returnedValues->values = [];
$returnedValues->success=true;
$returnedValues->errors=[];

foreach ($postObj->values as $values) {
    $id_entered = $values->id_comp;

    $strReq = "DELETE FROM `comp_courante`";
    $requete_supp=$db->query($strReq);

    $strReq = "INSERT INTO `comp_courante` (";
    $data = "(";

    $strReq .= " `id_comp` ";
    $data .= "'$values->id_comp'";

    $strReq .= ") VALUES $data )";

    $requete=$db->prepare($strReq);
    $statement = $requete->execute();
    $error = $requete->errorInfo();
    
    if ( $error[0] == '00000') {
        if( $requete->rowCount() != 0 ){
            $resultStr = "SELECT `id_comp` FROM `comp_courante` WHERE ";
            $resultStr .= " `id_comp`='$id_entered' ";

            $result=$db->query($resultStr);
            $row=$result->fetch(PDO::FETCH_OBJ);

            $obj = new stdClass();
            $obj->id_comp = $row->id_comp;

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
        $obj->error_desc = $error[2]; //enregistrement message d'erreru renvoyé
        $returnedValues->errors[] = $obj;
    }
}

echo json_encode($returnedValues);