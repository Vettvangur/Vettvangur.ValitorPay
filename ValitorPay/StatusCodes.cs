using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vettvangur.ValitorPay
{
    /**
        https://valitorpay.com/ResponseCodes.html
        let code = '';

        for (const tbody of document.getElementsByTagName('tbody'))
          for (const row of tbody.rows) {
            try {
              code += 'new ValitorStatusCode\r\n';
              code += '{\r\n';
              code += `    Code = \"${row.cells[0].outerText}\",\r\n`;
              code += `    Reason = \"${row.cells[1].outerText}\",\r\n`;
              code += '},\r\n';
              code += '\r\n';
            }
            catch (err) {
              console.error(err);
            }
          }
    */

    public static class StatusCodes
    {
        public static readonly IEnumerable<StatusCode> Codes = new StatusCode[]
        {
            #region Acquirer reject and error codes

            new StatusCode
            {
                Code = "A0",
                Reason = "User cannot be authenticated or action was not authorized.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "A1",
                Reason = "Invalid Card acceptor identification code (MerchantXID).",
            },

            new StatusCode
            {
                Code = "A2",
                Reason = "Invalid Category code.",
            },

            new StatusCode
            {
                Code = "A3",
                Reason = "Invalid Terminal ID or Device ID.",
            },

            new StatusCode
            {
                Code = "A4",
                Reason = "Invalid message ID.",
            },

            new StatusCode
            {
                Code = "A5",
                Reason = "Invalid card number.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "A6",
                Reason = "Invalid card expiration date.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "A7",
                Reason = "Invalid message UTC.",
            },

            new StatusCode
            {
                Code = "A8",
                Reason = "Invalid Currency (TransCurrency or OriginalTransCurrency value is invalid or field is not allowed for TransType).",
            },

            new StatusCode
            {
                Code = "A9",
                Reason = "Invalid Amount (TransAmount or OriginalTransAmount value is invalid or field is not allowed for TransType. TransFee invalid or not allowed for CategoryCode).",
            },

            new StatusCode
            {
                Code = "AA",
                Reason = "Invalid Message type.",
            },

            new StatusCode
            {
                Code = "AB",
                Reason = "Invalid Transaction type.",
            },

            new StatusCode
            {
                Code = "AC",
                Reason = "Invalid Card type.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "AD",
                Reason = "Invalid 3DSecure data (e.g. CAVD missing or wrong length).",
            },

            new StatusCode
            {
                Code = "AE",
                Reason = "Transaction rejected - wrong CardVD value (CVC2/CVV2).",
            },

            new StatusCode
            {
                Code = "AF",
                Reason = "Invalid Card verification data.",
            },

            new StatusCode
            {
                Code = "AG",
                Reason = "Invalid address verification data (AVSPostalCode or AVSStreetAddress).",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "AH",
                Reason = "Invalid Cashback amount.",
            },

            new StatusCode
            {
                Code = "AI",
                Reason = "Invalid Additional Data.",
            },

            new StatusCode
            {
                Code = "AJ",
                Reason = "Invalid Card (wrong issuer BIN or inactive BIN range).",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "AK",
                Reason = "Invalid Transaction date.",
            },

            new StatusCode
            {
                Code = "AL",
                Reason = "Invalid Card sequence number.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "AM",
                Reason = "Invalid Terminal capability/type.",
            },

            new StatusCode
            {
                Code = "AN",
                Reason = "Invalid Transaction time.",
            },

            new StatusCode
            {
                Code = "AO",
                Reason = "Invalid Transaction xid.",
            },

            new StatusCode
            {
                Code = "AP",
                Reason = "Invalid Authorization code.",
            },

            new StatusCode
            {
                Code = "AQ",
                Reason = "Invalid Track 2.",
            },

            new StatusCode
            {
                Code = "AR",
                Reason = "Invalid 3DSecure electronic commerce fields.",
            },

            new StatusCode
            {
                Code = "AS",
                Reason = "Invalid Icc data.",
            },

            new StatusCode
            {
                Code = "AV",
                Reason = "Connection to Visa EU/Mastercard is temporarily unavailable.",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "AW",
                Reason = "Time-out (authorization host received no response from Visa/Mastercard).",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "AY",
                Reason = "Unexpected error.",
            },

            new StatusCode
            {
                Code = "AZ",
                Reason = "Invalid Character(s) in Input Data (not covered by any of the codes above). Can apply to MessageCode, TransactionId, LifeCycleId, TransitIndicator, MerchantVerificationValue or Mastercard Wallet Identifier.",
            },

            new StatusCode
            {
                Code = "AX",
                Reason = "Blocked by firewall.",
            },

        #endregion

            #region Transaction related reject codes

            new StatusCode
            {
                Code = "T0",
                Reason = "Unable to locate previous message.",
            },

            new StatusCode
            {
                Code = "T1",
                Reason = "Previous message located, but the reversal data are inconsistent with original message.",
            },

            new StatusCode
            {
                Code = "T2",
                Reason = "No action taken (authorization already finalized).",
                Type = CodeType.Success,
            },

            new StatusCode
            {
                Code = "T3",
                Reason = "Duplicate transaction.",
                Type = CodeType.Success,
            },

            new StatusCode
            {
                Code = "T4",
                Reason = "Unable to confirm online pin.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "T5",
                Reason = "Initiation reason missing or invalid.",
            },

            new StatusCode
            {
                Code = "T6",
                Reason = "Initiator missing or invalid.",
            },

            new StatusCode
            {
                Code = "T7",
                Reason = "Dynamic Currency Conversion (DCC) not allowed on card.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "T8",
                Reason = "SCA exemption failed validation or exemption not allowed on transtype.",
            },

            new StatusCode
            {
                Code = "T9",
                Reason = "TransStatus not allowed for transaction.",
            },

            new StatusCode
            {
                Code = "TA",
                Reason = "Unsupported BIN.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "TB",
                Reason = "Acquirer declined SCA exemption.",
            },

            new StatusCode
            {
                Code = "TC",
                Reason = "Transaction being reversed was declined by the acquirer. No need to reverse.",
            },

        //new ValitorStatusCode
        //{
        //    Code = "TD...TZ",
        //    Reason = "Reserved for future use, however, processors/merchants should be prepared to receive them and interpret them as declined. Valitor may introduce new codes with short notice if necessary.",
        //},

        #endregion

            #region Merchant related reject codes

            new StatusCode
            {
                Code = "M0",
                Reason = "Merchant ID not found.",
            },

            new StatusCode
            {
                Code = "M1",
                Reason = "Merchant account closed.",
            },

            new StatusCode
            {
                Code = "M2",
                Reason = "Terminal ID not found.",
            },

            new StatusCode
            {
                Code = "M3",
                Reason = "Terminal closed.",
            },

            new StatusCode
            {
                Code = "M4",
                Reason = "Invalid category code.",
            },

            new StatusCode
            {
                Code = "M5",
                Reason = "Invalid currency.",
            },

            new StatusCode
            {
                Code = "M6",
                Reason = "Missing CVV2/CVC2.",
            },

            new StatusCode
            {
                Code = "M7",
                Reason = "CVV2 not allowed.",
            },

            new StatusCode
            {
                Code = "M8",
                Reason = "Merchant not registered for Verified by Visa/Secure Code.",
            },

            new StatusCode
            {
                Code = "M9",
                Reason = "Merchant not registered for Amex.",
            },

            new StatusCode
            {
                Code = "MA",
                Reason = "Transaction not permitted at terminal.",
            },

            new StatusCode
            {
                Code = "MB",
                Reason = "Agreement and terminal are not related.",
            },

            new StatusCode
            {
                Code = "MC",
                Reason = "Invalid processor ID.",
            },

            new StatusCode
            {
                Code = "MD",
                Reason = "Invalid merchant data (Name, City or Postal Code).",
            },

            new StatusCode
            {
                Code = "ME",
                Reason = "Sub-merchant account closed.",
            },

        //new ValitorStatusCode
        //{
        //    Code = "MF...MZ",
        //    Reason = "Reserved for future use, however, processors/merchants should be prepared to receive them and interpret them as declined. Valitor may introduce new codes with short notice if necessary.",
        //},

        #endregion

            #region Response codes for Visa transactions

            new StatusCode
            {
                Code = "00",
                Reason = "Approval.",
                Type = CodeType.Success,
            },

            new StatusCode
            {
                Code = "01",
                Reason = "Refer to card issuer.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "02",
                Reason = "Refer to card issuer, special condition.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "03",
                Reason = "Invalid merchant or service provider.",
            },

            new StatusCode
            {
                Code = "04",
                Reason = "Pickup card.",
                Type = CodeType.SecurityEvent,
            },

            new StatusCode
            {
                Code = "05",
                Reason = "Do not honor.",
                Type = CodeType.SecurityEvent,
           },

            new StatusCode
            {
                Code = "06",
                Reason = "Error.",
            },

            new StatusCode
            {
                Code = "07",
                Reason = "Pick up card, special condition (other than lost/stolen card).",
                Type = CodeType.SecurityEvent,
            },

            new StatusCode
            {
                Code = "10",
                Reason = "Partial Approval.",
                Type = CodeType.Success,
            },

            new StatusCode
            {
                Code = "11",
                Reason = "V.I.P approval.",
                Type = CodeType.Success,
            },

            new StatusCode
            {
                Code = "12",
                Reason = "Invalid transaction.",
            },

            new StatusCode
            {
                Code = "13",
                Reason = "Invalid amount (currency conversion field overflow) or amount exceeds maximum for card program.",
            },

            new StatusCode
            {
                Code = "14",
                Reason = "Invalid account number (no such number).",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "15",
                Reason = "No such issuer.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "19",
                Reason = "Re-enter transaction.",
                Type = CodeType.UserError,
                Retryable = true,
            },

            new StatusCode
            {
                Code = "21",
                Reason = "No action taken (unable to back out prior transaction).",
            },

            new StatusCode
            {
                Code = "25",
                Reason = "Unable to locate record in file, or account number is missing from the inquiry.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "28",
                Reason = "File is temporarily unavailable.",
            },

            new StatusCode
            {
                Code = "39",
                Reason = "No credit account.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "41",
                Reason = "Pickup card (lost card).",
                Type = CodeType.SecurityEvent,
            },

            new StatusCode
            {
                Code = "43",
                Reason = "Pickup card (stolen card).",
                Type = CodeType.SecurityEvent,
            },

            new StatusCode
            {
                Code = "46",
                Reason = "Closed account.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "51",
                Reason = "Insufficient funds.",
                Type = CodeType.InsufficientFunds,
            },

            new StatusCode
            {
                Code = "52",
                Reason = "No checking account.",
            },

            new StatusCode
            {
                Code = "53",
                Reason = "No savings account.",
            },

            new StatusCode
            {
                Code = "54",
                Reason = "Expired card.",
                Type = CodeType.UserError,
           },

            new StatusCode
            {
                Code = "55",
                Reason = "Incorrect PIN.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "57",
                Reason = "Transaction not permitted to cardholder.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "58",
                Reason = "Transaction not allowed at terminal.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "59",
                Reason = "Suspected fraud.",
                Type = CodeType.SecurityEvent,
            },

            new StatusCode
            {
                Code = "61",
                Reason = "Activity amount limit exceeded.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "62",
                Reason = "Restricted card (e.g. in Country Exclusion table).",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "63",
                Reason = "Security violation.",
                Type = CodeType.SecurityEvent,
            },

            new StatusCode
            {
                Code = "64",
                Reason = "Transaction does not fulfill AML requirement.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "65",
                Reason = "Additional customer authentication required (formerly: Exceeds withdrawal count limit).",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "70",
                Reason = "PIN Data Required.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "75",
                Reason = "Allowable number of PIN-entry tries exceeded.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "76",
                Reason = "Unable to locate previous message (no match on transaction ID).",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "77",
                Reason = "Previous message located, but the reversal data are inconsistent with original message.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "78",
                Reason = "\"Blocked, first used\" - Transaction from new cardholder, and card not properly unblocked.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "79",
                Reason = "Transaction reversed.",
            },

            new StatusCode
            {
                Code = "80",
                Reason = "Visa transactions: credit issuer unavailable. Private label: invalid date.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "81",
                Reason = "PIN cryptographic error found (error found by VIC security module during PIN decryption).",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "82",
                Reason = "Negative Online CAM, dCVV, iCVV, or CVV results or offline PIN authentication interrupted.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "85",
                Reason = "No reason to decline a request for Account Number verification, address verification or CVV2 verification.",
            },

            new StatusCode
            {
                Code = "86",
                Reason = "Cannot Verify PIN.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "91",
                Reason = "Issuer unavailable or switch inoperative (STIP not applicable or available for this transaction).",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "92",
                Reason = "Financial institution or intermediate network facility cannot be found for routing.",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "93",
                Reason = "Transaction cannot be completed; violation of law.",
                Type = CodeType.SecurityEvent,
            },

            new StatusCode
            {
                Code = "94",
                Reason = "Duplicate transaction.",
            },

            new StatusCode
            {
                Code = "96",
                Reason = "System malfunction.",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "N0",
                Reason = "Force STIP.",
            },

            new StatusCode
            {
                Code = "N3",
                Reason = "Cash service not available.",
            },

            new StatusCode
            {
                Code = "N4",
                Reason = "Cashback request exceeds Issuer limit.",
            },

            new StatusCode
            {
                Code = "N7",
                Reason = "Decline for CVV2 failure.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "N8",
                Reason = "Transaction amount exceeds pre-authorized approval amount.",
                Type = CodeType.InsufficientFunds,
            },

            new StatusCode
            {
                Code = "P2",
                Reason = "Invalid biller information.",
            },

            new StatusCode
            {
                Code = "P5",
                Reason = "PIN change/unblock request declined.",
            },

            new StatusCode
            {
                Code = "P6",
                Reason = "Unsafe PIN.",
                Type = CodeType.SecurityEvent,
            },

            new StatusCode
            {
                Code = "R0",
                Reason = "Stop Payment Order.",
            },

            new StatusCode
            {
                Code = "R1",
                Reason = "Revocation of Authorization.",
            },

            new StatusCode
            {
                Code = "R3",
                Reason = "Revocation of All Authorizations.",
            },

            new StatusCode
            {
                Code = "Z3",
                Reason = "Unable to go online.",
            },

            new StatusCode
            {
                Code = "XA",
                Reason = "Forward to issuer.",
            },

            new StatusCode
            {
                Code = "XD",
                Reason = "Forward to issuer.",
            },

            new StatusCode
            {
                Code = "Q1",
                Reason = "Card authentication failed. Or offline PIN authentication interrupted.",
            },

            new StatusCode
            {
                Code = "1A",
                Reason = "Additional customer authentication required.",
                Type = CodeType.UserError,
            },

            #endregion

            #region Response codes for Mastercard transactions

            //new ValitorStatusCode
            //{
            //    Code = "00",
            //    Reason = "Approved or completed successfully.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "01",
            //    Reason = "Refer to card issuer.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "03",
            //    Reason = "Invalid merchant or service provider.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "04",
            //    Reason = "Capture card.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "05",
            //    Reason = "Do not honor.",
            //},

            new StatusCode
            {
                Code = "08",
                Reason = "Honor with ID.",
                Type = CodeType.Success,
            },

            //new ValitorStatusCode
            //{
            //    Code = "10",
            //    Reason = "Partial Approval.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "12",
            //    Reason = "Invalid transaction.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "13",
            //    Reason = "Invalid amount.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "14",
            //    Reason = "Invalid card number.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "15",
            //    Reason = "Invalid issuer.",
            //},

            new StatusCode
            {
                Code = "30",
                Reason = "Format error.",
            },

            //new ValitorStatusCode
            //{
            //    Code = "41",
            //    Reason = "Lost card.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "43",
            //    Reason = "Stolen card.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "51",
            //    Reason = "Insufficient funds/over credit limit.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "54",
            //    Reason = "Expired card.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "55",
            //    Reason = "Invalid PIN.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "57",
            //    Reason = "Transaction not permitted to cardholder.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "58",
            //    Reason = "Transaction not permitted to acquirer/terminal.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "61",
            //    Reason = "Exceeds withdrawal amount limit.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "62",
            //    Reason = "Restricted card.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "63",
            //    Reason = "Security violation.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "65",
            //    Reason = "Additional customer authentication required (formerly: Exceeds withdrawal count limit).",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "70",
            //    Reason = "Contact Card Issuer.",
            //},

            new StatusCode
            {
                Code = "71",
                Reason = "PIN not changed.",
                Type = CodeType.UserError,
            },

            //new ValitorStatusCode
            //{
            //    Code = "75",
            //    Reason = "Allowable number of PIN tries exceeded.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "76",
            //    Reason = "Invalid/nonexistent \"To Account\" specified.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "77",
            //    Reason = "Invalid/nonexistent \"From Account\" specified.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "78",
            //    Reason = "Invalid/nonexistent account specified.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "81",
            //    Reason = "Domestic debit transaction not allowed.",
            //},

            new StatusCode
            {
                Code = "84",
                Reason = "Invalid authorization life cycle.",
            },

            //new ValitorStatusCode
            //{
            //    Code = "85",
            //    Reason = "Not declined (Valid for all zero amount transactions.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "86",
            //    Reason = "PIN validation not possible.",
            //},

            new StatusCode
            {
                Code = "87",
                Reason = "Purchase amount only, no cash back allowed.",
            },

            new StatusCode
            {
                Code = "88",
                Reason = "Cryptographic failure.",
            },

            new StatusCode
            {
                Code = "89",
                Reason = "Unacceptable PIN.",
                Type = CodeType.UserError,
            },

            //new ValitorStatusCode
            //{
            //    Code = "91",
            //    Reason = "Authorization system or issuer inoperative.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "92",
            //    Reason = "Unable to route transaction.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "94",
            //    Reason = "Duplicate transmission detected.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "96",
            //    Reason = "System error.",
            //},

            #endregion

            #region Response codes for Amex transactions

            //new ValitorStatusCode
            //{
            //    Code = "00",
            //    Reason = "Approval.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "05",
            //    Reason = "Do not honor.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "10",
            //    Reason = "Partial Approval.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "13",
            //    Reason = "Invalid amount.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "14",
            //    Reason = "Invalid account number (no such number).",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "30",
            //    Reason = "Format error.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "54",
            //    Reason = "Expired card.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "55",
            //    Reason = "Incorrect PIN.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "75",
            //    Reason = "Allowable number of PIN tries exceeded.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "91",
            //    Reason = "Issuer unavailable.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "96",
            //    Reason = "System error.",
            //},

            //new ValitorStatusCode
            //{
            //    Code = "1A",
            //    Reason = "Additional customer identification required.",
            //},

            #endregion

            #region Processor related reject codes

            new StatusCode
            {
                Code = "AT",
                Reason = "Acquirer timed-out.",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "AU",
                Reason = "Acquirer unavailable.",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "B1",
                Reason = "Success fetching and processing virtual card data.",
                Type = CodeType.Success,
           },

            new StatusCode
            {
                Code = "B2",
                Reason = "Unable to fetch virtual card data.",
            },

            new StatusCode
            {
                Code = "B3",
                Reason = "Unexpected error when fetching virtual card data.",
            },

            new StatusCode
            {
                Code = "B4",
                Reason = "Initiator or initiator reason do not match with the virtual card found.",
            },

            new StatusCode
            {
                Code = "P1",
                Reason = "Merchant initiated transactions or virtual cards, can not be authorized with 3dSecure data.",
            },

            new StatusCode
            {
                Code = "PN",
                Reason = "Payout is not allowed to merchant.",
            },

            new StatusCode
            {
                Code = "PP",
                Reason = "Processing communication error.",
            },

            // Overlap
            //new ValitorStatusCode
            //{
            //    Code = "Q1",
            //    Reason = "Origin host name could not be retrieved or it's not allowed.",
            //},

            new StatusCode
            {
                Code = "Q2",
                Reason = "Payment can not be completed because call to payment system failed.",
            },

            new StatusCode
            {
                Code = "Q3",
                Reason = "Sending of success payment notification failed for the provided webhook endpoint.",
            },

            new StatusCode
            {
                Code = "Q4",
                Reason = "Cardholder was not successfully 3DSecure authenticated.",
            },

            new StatusCode
            {
                Code = "S2",
                Reason = "Merchant data was not found.",
            },

            new StatusCode
            {
                Code = "S3",
                Reason = "Unexpected error occured when fetching merchant data.",
            },

            new StatusCode
            {
                Code = "UB",
                Reason = "It is not possible to get updated account for this bin at this time.",
            },

            new StatusCode
            {
                Code = "UD",
                Reason = "Account was found but insufficient card data was provided.",
            },

            new StatusCode
            {
                Code = "UI",
                Reason = "Internal Service Error while updating account.",
            },

            new StatusCode
            {
                Code = "UM",
                Reason = "Transaction ID and Transaction Date must be supplied for Valitor issued cards.",
            },

            new StatusCode
            {
                Code = "UN",
                Reason = "No updated account was found for the given card number.",
            },

            new StatusCode
            {
                Code = "V1",
                Reason = "Success creating merchant virtual card.",
                Type = CodeType.Success,
            },

            new StatusCode
            {
                Code = "V2",
                Reason = "Unexpected error, please contact support.",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "V3",
                Reason = "Card was not fully authenticated and therefore virtual card can not be created.",
            },

            new StatusCode
            {
                Code = "X1",
                Reason = "DCC approved.",
            Type = CodeType.Success,
            },

            new StatusCode
            {
                Code = "X2",
                Reason = "DCC Transaction not allowed to Cardholder.",
            },

            new StatusCode
            {
                Code = "X3",
                Reason = "DCC Cardholder currency not supported.",
            },

            new StatusCode
            {
                Code = "X4",
                Reason = "DCC Exceeds time limit for withdrawal (too late).",
            },

            new StatusCode
            {
                Code = "X5",
                Reason = "DCC Transaction not allowed to terminal equipment.",
            },

            new StatusCode
            {
                Code = "X6",
                Reason = "DCC not allowed to merchant.",
            },

            new StatusCode
            {
                Code = "X7",
                Reason = "DCC Unknown error.",
            },

            #endregion

            #region Get batch report response codes

            new StatusCode
            {
                Code = "D1",
                Reason = "Success response code.",
            },

            new StatusCode
            {
                Code = "D2",
                Reason = "Batch report is not yet available for this batch ID. Please try again later.",
            },

            new StatusCode
            {
                Code = "D3",
                Reason = "The batch with this batch ID has been deleted.",
            },

            new StatusCode
            {
                Code = "D4",
                Reason = "The batch with this batch ID has been closed.",
            },

            new StatusCode
            {
                Code = "D5",
                Reason = "The batch with this batch ID is open, the batch report has not been created yet.",
            },

            new StatusCode
            {
                Code = "D6",
                Reason = "The batch with this batch ID is beeing processed, the batch report has not been created yet.",
            },

            new StatusCode
            {
                Code = "D7",
                Reason = "The batch with this batch ID has expired.",
            },

            new StatusCode
            {
                Code = "D8",
                Reason = "The batch file with this batch ID has been received.",
            },

            new StatusCode
            {
                Code = "D9",
                Reason = "The batch file with this batch ID has been parsed successfully.",
            },

            new StatusCode
            {
                Code = "D0",
                Reason = "The batch file with this batch ID has been successfully validated.",
            },

            new StatusCode
            {
                Code = "DA",
                Reason = "The batch file with this batch ID has been successfully saved.",
            },

            new StatusCode
            {
                Code = "DB",
                Reason = "The batch file with this batch ID has been sent.",
            },

            new StatusCode
            {
                Code = "DC",
                Reason = "The batch file with this batch ID had parsing errors.",
            },

            new StatusCode
            {
                Code = "DD",
                Reason = "The batch file with this batch ID had validation errors.",
            },

            new StatusCode
            {
                Code = "DE",
                Reason = "There was an error sending the batch file with this batch.",
            },

            new StatusCode
            {
                Code = "DF",
                Reason = "There was an error saving the batch file with this batch ID.",
            },

            #endregion

            #region 3D Secure response codes

            new StatusCode
            {
                Code = "C1",
                Reason = "3DSecure verification was a success.",
                Type = CodeType.Success,
            },

            new StatusCode
            {
                Code = "C2",
                Reason = "3DSecure verification failed, merchant is not enrolled in 3DSecure.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "C3",
                Reason = "3DSecure verification failed, unable to validate card.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "C4",
                Reason = "3DSecure verification failed, could not determine card type.",
                Type = CodeType.UserError,
            },

            new StatusCode
            {
                Code = "C5",
                Reason = "There was a problem receiving data from 3dSecure MPI, please try again later.",
                Retryable = true,
            },

            new StatusCode
            {
                Code = "C6",
                Reason = "Validation error from MPI.",
            },

            new StatusCode
            {
                Code = "C7",
                Reason = "There was an error in response from MPI.",
            },

            new StatusCode
            {
                Code = "C8",
                Reason = "The card does not support 3DSecure.",
                Type = CodeType.UserError,
            },

            #endregion
        };

        public static IReadOnlyDictionary<string, StatusCode> CodeMap
            = Codes.ToDictionary(x => x.Code, x => x);

        public static bool IsRetryableErrorCode(ValitorResponseBase resp)
            => CodeMap.ContainsKey(resp.ResponseCode) && CodeMap[resp.ResponseCode].Retryable;
    }

    public static class MDStatusCodes
    {
        public static readonly IEnumerable<MdStatusCode> Codes = new MdStatusCode[]
        {
            new MdStatusCode
            {
                Code = 0,

                Reason = "Not Authenticated, do not continue transaction",

                Type = CodeType.UserError,
            },

            new MdStatusCode
            {
                Code = 1,

                Reason = "Fully Authenticated, continue transaction",

                Type = CodeType.Success,
            },

            new MdStatusCode
            {
                Code = 2,

                Reason = "Not enrolled (3DS1 only, may continue to transaction)",

                Type = CodeType.Success,
            },

            new MdStatusCode
            {
                Code = 3,

                Reason = "Not enrolled cache (not used, was used in early 3DS1)",

                Type = CodeType.Success,
            },

            new MdStatusCode
            {
                Code = 4,

                Reason = "Attempt (Proof of authentication attempt, may continue to transaction)",

                Type = CodeType.Success,
            },

            new MdStatusCode
            {
                Code = 5,

                Reason = "U-received (grey area, continue to transaction depends schema rules)",
            },

            new MdStatusCode
            {
                Code = 6,

                Reason = "Error received (from Directory or ACS)",

                Retryable = true,
            },

            new MdStatusCode
            {
                Code = 7,

                Reason = "Error occured on MPI side, this may happen when the input data is invalid, this may also be returned if the MPI does not support 3DSecure for the particular card brand.",

                Retryable = true,
            },

            new MdStatusCode
            {
                Code = 8,

                Reason = "Block by Fraud Score (used only if set up with FSS and scores configured)",

                Type = CodeType.SecurityEvent,
            },

            new MdStatusCode
            {
                Code = 9,

                Reason = "Pending (this code is not sent in red, internal, status only, except XML API or SDK in challenge)",
            },

            new MdStatusCode
            {
                Code = 50,

                Reason = "Execute 3DS method and continue to authentication (XML API or SOAP only in return EnrollmentRequest(initial)).",
            },

            new MdStatusCode
            {
                Code = 51,

                Reason = "No 3DS method and continue to authentication (XML API or SOAP only in return).",
            },

            new MdStatusCode
            {
                Code = 60,

                Reason = "Action completed, not sent only used internally for PReq result updates",
            },

            new MdStatusCode
            {
                Code = 80,

                Reason = "Skip device case (no 3DS performed as device known not well capable)",
            },

            new MdStatusCode
            {
                Code = 81,

                Reason = "Skip challenge because requestor challenge ind (02 = No challenge requested)",
            },

            new MdStatusCode
            {
                Code = 88,

                Reason = "Skip 3DS because low risk (used only if with FSS and scores configured)",

                Type = CodeType.Success,
            },

            new MdStatusCode
            {
                Code = 91,

                Reason = "Network error",

                Retryable = true,
            },

            new MdStatusCode
            {
                Code = 92,

                Reason = "Directory error (read timeout)",

                Retryable = true,
            },

            new MdStatusCode
            {
                Code = 93,

                Reason = "Configuration error",

                Retryable = true,
            },

            new MdStatusCode
            {
                Code = 94,

                Reason = "Merchant input errors (also if merchant posted CRes not matching current tx state by RReq)",

                Retryable = true,
            },

            new MdStatusCode
            {
                Code = 95,

                Reason = "No directory found for PAN/cardtype",
            },

            new MdStatusCode
            {
                Code = 96,

                Reason = "No version 2 directory found for PAN/cardtype",
            },

            new MdStatusCode
            {
                Code = 97,

                Reason = "If transaction not found on continue or service query",

                Retryable = true,
            },

            new MdStatusCode
            {
                Code = 99,

                Reason = "System error (mpi unexpected error)",

                Retryable = true,
            },
        };

        public static IReadOnlyDictionary<int, MdStatusCode> CodeMap
            = Codes.ToDictionary(x => x.Code, x => x);
    }

    public class MdStatusCode : IStatusCode
    {
        public int Code { get; set; }

        public string Reason { get; set; }

        public bool Retryable { get; set; }

        public CodeType Type { get; set; }
    }

    public class StatusCode : IStatusCode
    {
        public string Code { get; set; }

        public string Reason { get; set; }

        public bool Retryable { get; set; }

        public CodeType Type { get; set; }
    }

    public interface IStatusCode
    {
        string Reason { get; set; }

        bool Retryable { get; set; }

        CodeType Type { get; set; }
    }

    public enum CodeType 
    {
        /// <summary>
        /// In these cases the user might be able to retry if the code is marked as such, in others he may simply have to wait.
        /// Usually no reason to try a different card
        /// </summary>
        ServerError,

        /// <summary>
        /// In these cases the user should try a different card or complete some action with his/her card
        /// F.x. insufficient funds, new card with incomplete configuration
        /// </summary>
        UserError,

        /// <summary>
        /// Stolen cards, frauds, and similar blocks
        /// </summary>
        SecurityEvent,

        /// <summary>
        /// Indicate successful transactions
        /// </summary>
        Success,

        InsufficientFunds,

        /// <summary>
        /// Not implemented, non-error, non-success messages. F.x. continue transaction messages
        /// </summary>
        StatusMessage = 99,
    }
}
