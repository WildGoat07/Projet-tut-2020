<?php
require_once 'app/Database.php';

header('Content-Type: application/json');

$db = new Database();

if ($db) {
    $strReq = "SELECT `code_ec`, `annee`, `eff_prev`, `eff_reel`, `GpCM`, `GpEI`, `GpTD`, `GpTP`, `GpTPL`, `GpPRJ`
    , `GpCMSer`, `GpEISer`, `GpTDSer`, `GpTPLSer`, `GpPRJSer` FROM `enseignement`";
    $postObj = json_decode(file_get_contents('php://input'));
    if (isset($postObj->filters)) {
        $firstFilter = true;
        $whereSet = false;
        if (isset($postObj->filters->code_ec)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->code_ec as $code_ec) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`code_ec` = \"$code_ec\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->annee)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->annee as $annee) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`annee` = \"$annee\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->eff_prev)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->eff_prev as $eff_prev) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`eff_prev` = \"$eff_prev\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->eff_reel)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            $firstArrayFilter = true;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $strReq .= '(';
            foreach ($postObj->filters->eff_reel as $eff_reel) {
                if (!$firstArrayFilter)
                    $strReq .= " OR ";
                $strReq .= "`eff_reel` = \"$eff_reel\"";
                $firstArrayFilter = false;
            }
            $strReq .= ')';
        }
        if (isset($postObj->filters->GpCM)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpCM->min)) {
                $strReq .= "`GpCM` >= " . $postObj->filters->GpCM->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpCM->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpCM` <= " . $postObj->filters->GpCM->max;
            }
        }
        if (isset($postObj->filters->GpEI)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpEI->min)) {
                $strReq .= "`GpEI` >= " . $postObj->filters->GpEI->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpEI->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpEI` <= " . $postObj->filters->GpEI->max;
            }
        }
        //---------------------------------------------------
        if (isset($postObj->filters->GpTD)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpTD->min)) {
                $strReq .= "`GpTD` >= " . $postObj->filters->GpTD->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpTD->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpTD` <= " . $postObj->filters->GpTD->max;
            }
        }
        if (isset($postObj->filters->GpTP)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpTP->min)) {
                $strReq .= "`GpTP` >= " . $postObj->filters->GpTP->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpTP->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpTP` <= " . $postObj->filters->GpTP->max;
            }
        }
        if (isset($postObj->filters->GpTPL)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpTPL->min)) {
                $strReq .= "`GpTPL` >= " . $postObj->filters->GpTPL->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpTPL->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpTPL` <= " . $postObj->filters->GpTPL->max;
            }
        }
        if (isset($postObj->filters->GpPRJ)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpPRJ->min)) {
                $strReq .= "`GpPRJ` >= " . $postObj->filters->GpPRJ->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpPRJ->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpPRJ` <= " . $postObj->filters->GpPRJ->max;
            }
        }
        if (isset($postObj->filters->GpCMSer)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpCMSer->min)) {
                $strReq .= "`GpCMSer` >= " . $postObj->filters->GpCMSer->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpCMSer->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpCMSer` <= " . $postObj->filters->GpCMSer->max;
            }
        }
        if (isset($postObj->filters->GpEISer)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpEISer->min)) {
                $strReq .= "`GpEISer` >= " . $postObj->filters->GpEISer->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpEISer->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpEISer` <= " . $postObj->filters->GpEISer->max;
            }
        }
        if (isset($postObj->filters->GpTDSer)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpTDSer->min)) {
                $strReq .= "`GpTDSer` >= " . $postObj->filters->GpTDSer->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpTDSer->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpTDSer` <= " . $postObj->filters->GpTDSer->max;
            }
        }
        if (isset($postObj->filters->GpTPSer)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpTPSer->min)) {
                $strReq .= "`GpTPSer` >= " . $postObj->filters->GpTPSer->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpTPSer->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpTPSer` <= " . $postObj->filters->GpTPSer->max;
            }
        }
        if (isset($postObj->filters->GpTPLSer)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpTPLSer->min)) {
                $strReq .= "`GpTPLSer` >= " . $postObj->filters->GpTPLSer->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpTPLSer->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpTPLSer` <= " . $postObj->filters->GpTPLSer->max;
            }
        }
        if (isset($postObj->filters->GpPRJSer)) {
            if (!$firstFilter)
                $strReq .= " AND ";
            $firstFilter = false;
            if (!$whereSet) {
                $strReq .= " WHERE ";
                $whereSet = true;
            }
            $minSet = false;
            if (isset($postObj->filters->GpPRJSer->min)) {
                $strReq .= "`GpPRJSer` >= " . $postObj->filters->GpPRJSer->min;
                $minSet = true;
            }
            if (isset($postObj->filters->GpPRJSer->max)) {
                if ($minSet)
                    $strReq .= " AND ";
                $strReq .= "`GpPRJSer` <= " . $postObj->filters->GpPRJSer->max;
            }
        }
    }

    if (isset($postObj->order))
        if (isset($postObj->reverse_order) && $postObj->reverse_order)
            $strReq .= " ORDER BY DESC `$postObj->order`";
        else
            $strReq .= " ORDER BY `$postObj->order`";
    else if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY DESC `code_ec`";
    else
        $strReq .= " ORDER BY `code_ec`";
    $strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

    $requete = $db->query($strReq);

    $enseignement = new stdClass();
    $enseignement->values = [];

    foreach ($requete as $req) {
        $obj = new stdClass();

        $obj->code_ec = utf8_encode($req['code_ec']);

        $obj->annee = utf8_encode($req['annee']);

        $obj->eff_prev = utf8_encode($req['eff_prev']);

        $obj->eff_reel = utf8_encode($req['eff_reel']);

        $obj->GpCM = utf8_encode($req['GpCM']);

        $obj->GpEI = utf8_encode($req['GpEI']);

        $obj->GpTD = utf8_encode($req['GpTD']);

        $obj->GpTP = utf8_encode($req['GpTP']);

        $obj->GpTPL = utf8_encode($req['GpTPL']);

        $obj->GpPRJ = utf8_encode($req['GpPRJ']);

        $obj->GpCMSer = utf8_encode($req['GpCMSer']);

        $obj->GpEISer = utf8_encode($req['GpEISer']);

        $obj->GpTDSer = utf8_encode($req['GpTDSer']);

        $obj->GpTPSer = utf8_encode($req['GpTPSer']);

        $obj->GpTPLSer = utf8_encode($req['GpTPLSer']);

        $obj->GpPRJer = utf8_encode($req['GpPRJSer']);

        $enseignement->values[] = $obj;
    }

    $enseignement->success = true;

    echo json_encode($enseignement);
} else {
    echo json_encode($connectionDB);
}
