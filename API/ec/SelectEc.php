<?php
require_once '../app/Database.php';
require_once '../utilities.php';

header('Content-Type: application/json');

$ec = new stdClass();
$ec->values = [];
$ec->success = true;
$ec->errors = [];

$strReq = "SELECT `code_ec`, `libelle_ec`, `nature`, `HCM`, `HEI`, `HTD`, `HTP`, `HTPL`, `HPRJ`, `NbEpr`, `CNU`, `no_cat`, `code_ec_pere`, `code_ue` FROM `ec`";
$postObj = json_decode(file_get_contents('php://input'));

$whereSet = false;

if (isset($postObj->filters)) {
    $firstFilter = true;
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
    if (isset($postObj->filters->nature)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->nature as $nature) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`nature` = \"$nature\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->HCM)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HCM->min)) {
            $strReq .= "`HCM` >= " . $postObj->filters->HCM->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HCM->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HCM` <= " . $postObj->filters->HCM->max;
        }
    }
    if (isset($postObj->filters->HEI)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HEI->min)) {
            $strReq .= "`HEI` >= " . $postObj->filters->HEI->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HEI->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HEI` <= " . $postObj->filters->HEI->max;
        }
    }
    if (isset($postObj->filters->HTD)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HTD->min)) {
            $strReq .= "`HTD` >= " . $postObj->filters->HTD->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HTD->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HTD` <= " . $postObj->filters->HTD->max;
        }
    }
    if (isset($postObj->filters->HTP)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HTP->min)) {
            $strReq .= "`HTP` >= " . $postObj->filters->HTP->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HTP->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HTP` <= " . $postObj->filters->HTP->max;
        }
    }
    if (isset($postObj->filters->HTPL)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HTPL->min)) {
            $strReq .= "`HTPL` >= " . $postObj->filters->HTPL->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HTPL->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HTPL` <= " . $postObj->filters->HTPL->max;
        }
    }
    if (isset($postObj->filters->HPRJ)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->HPRJ->min)) {
            $strReq .= "`HPRJ` >= " . $postObj->filters->HPRJ->min;
            $minSet = true;
        }
        if (isset($postObj->filters->HPRJ->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`HPRJ` <= " . $postObj->filters->HPRJ->max;
        }
    }
    if (isset($postObj->filters->NbEpr)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $minSet = false;
        if (isset($postObj->filters->NbEpr->min)) {
            $strReq .= "`NbEpr` >= " . $postObj->filters->NbEpr->min;
            $minSet = true;
        }
        if (isset($postObj->filters->NbEpr->max)) {
            if ($minSet)
                $strReq .= " AND ";
            $strReq .= "`NbEpr` <= " . $postObj->filters->NbEpr->max;
        }
    }
    if (isset($postObj->filters->CNU)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->CNU as $CNU) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            $strReq .= "`CNU` = \"$CNU\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->no_cat)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->no_cat as $no_cat) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            if ($no_cat == null)
                $strReq .= "`no_cat` IS NULL";
            else
                $strReq .= "`no_cat` = \"$no_cat\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->code_ec_pere)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_ec_pere as $code_ec_pere) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            if ($code_ec_pere == null)
                $strReq .= "`code_ec_pere` IS NULL";
            else
                $strReq .= "`code_ec_pere` = \"$code_ec_pere\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
    if (isset($postObj->filters->code_ue)) {
        if (!$firstFilter)
            $strReq .= " AND ";
        $firstFilter = false;
        $firstArrayFilter = true;
        if (!$whereSet) {
            $strReq .= " WHERE ";
            $whereSet = true;
        }
        $strReq .= '(';
        foreach ($postObj->filters->code_ue as $code_ue) {
            if (!$firstArrayFilter)
                $strReq .= " OR ";
            if ($code_ue == null)
                $strReq .= "`code_ue` IS NULL";
            else
                $strReq .= "`code_ue` = \"$code_ue\"";
            $firstArrayFilter = false;
        }
        $strReq .= ')';
    }
}

if (isset($postObj->search)) {
    $strReq .= $whereSet ? " AND " : " WHERE ";

    $search = cleanString($postObj->search);

    $strReq .= " compareStrings(\"$search\", `libelle_ec`) ";
}

if (isset($postObj->order))
    if (isset($postObj->reverse_order) && $postObj->reverse_order)
        $strReq .= " ORDER BY `$postObj->order` DESC ";
    else
        $strReq .= " ORDER BY `$postObj->order`";
else if (isset($postObj->reverse_order) && $postObj->reverse_order)
    $strReq .= " ORDER BY `code_ec` DESC ";
else
    $strReq .= " ORDER BY `code_ec`";
$strReq .= "LIMIT $postObj->quantity OFFSET $postObj->skip";

$requete = $db->prepare($strReq);
$statement = $requete->execute();
$error = $requete->errorInfo();

if ($error[0] == '00000') {
    if ($requete->rowCount() != 0) {
        foreach ($requete as $req) {
            $obj = new stdClass();

            $obj->code_ec = utf8_encode($req['code_ec']);

            $obj->libelle_ec = utf8_encode($req['libelle_ec']);

            $obj->nature = utf8_encode($req['nature']);

            $obj->HCM = utf8_encode($req['HCM']);

            $obj->HEI = utf8_encode($req['HEI']);

            $obj->HTD = utf8_encode($req['HTD']);

            $obj->HTP = utf8_encode($req['HTP']);

            $obj->HTPL = utf8_encode($req['HTPL']);

            $obj->HPRJ = utf8_encode($req['HPRJ']);

            $obj->NbEpr = utf8_encode($req['NbEpr']);

            $obj->CNU = utf8_encode($req['CNU']);

            $obj->no_cat = $req['no_cat'] == null ? null : utf8_encode($req['no_cat']);

            $obj->code_ec_pere = $req['code_ec_pere'] == null ? null : utf8_encode($req['code_ec_pere']);

            $obj->code_ue = $req['code_ue'] == null ? null : utf8_encode($req['code_ue']);

            $ec->values[] = $obj;
        }
    }
} else {
    $ec->success = false;

    $obj = new stdClass();
    $obj->error_code = $error[0];
    $obj->error_desc = $error[2];
    $ec->errors[] = $obj;
}

echo json_encode($ec);
