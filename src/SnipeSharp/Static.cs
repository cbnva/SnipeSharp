using System.Text;

namespace SnipeSharp
{
    internal static class Static
    {
        internal readonly static Encoding UTF8NoBom = new UTF8Encoding(false);
        internal const string ASSET_NAME = "asset_name";
        internal const string ASSET_TAG = "asset_tag";
        internal const string ASSIGNED_PIVOT_ID = "assigned_pivot_id";
        internal const string AVAILABLE_ACTIONS = "available_actions";
        internal const string CHECKOUT_NOTES = "checkout_notes";
        internal const string COST = "cost";
        internal const string CREATED_AT = "created_at";
        internal const string CURRENCY = "currency";
        internal const string DELETED_AT = "deleted_at";
        internal const string EOL = "eol";
        internal const string EXPECTED_CHECKIN = "expected_checkin";
        internal const string ID = "id";
        internal const string IMAGE = "image";
        internal const string LAST_CHECKOUT = "last_checkout";
        internal const string LIMIT = "limit";
        internal const string MANAGER = "manager";
        internal const string MODEL_NUMBER = "model_number";
        internal const string NAME = "name";
        internal const string NOTES = "notes";
        internal const string OFFSET = "offset";
        internal const string ORDER = "order";
        internal const string PERMISSIONS = "permissions";
        internal const string QUANTITY = "qty";
        internal const string REQUESTABLE = "requestable";
        internal const string SEARCH = "search";
        internal const string SERIAL = "serial";
        internal const string SORT_COLUMN = "sort";
        internal const string STATUS = "status";
        internal const string TITLE = "title";
        internal const string TYPE = "type";
        internal const string UPDATED_AT = "updated_at";
        internal const string USERNAME = "username";
        internal const string URL = "url";

        internal static class Types
        {
            internal const string ASSET = "asset";
            internal const string COMPANY = "company";
            internal const string CATEGORY = "category";
            internal const string DEPARTMENT = "department";
            internal const string DEPRECIATION = "depreciation";
            internal const string FIELDSET = "fieldset";
            internal const string LICENSE = "license";
            internal const string LOCATION = "location";
            internal const string GROUP = "groups";
            internal const string MANUFACTURER = "manufacturer";
            internal const string MODEL = "model";
            internal const string SUPPLIER = "supplier";
            internal const string USER = "user";
        }

        internal static class Id
        {
            internal const string ASSET = Types.ASSET + "_" + ID;
            internal const string CATEGORY = Types.CATEGORY + "_" + ID;
            internal const string COMPANY = Types.COMPANY + "_" + ID;
            internal const string DEPARTMENT = Types.DEPARTMENT + "_" + ID;
            internal const string GROUP = Types.GROUP + "_" + ID;
            internal const string LICENSE = Types.LICENSE + "_" + ID;
            internal const string LOCATION = Types.LOCATION + "_" + ID;
            internal const string MANUFACTURER = Types.MANUFACTURER + "_" + ID;
            internal const string SUPPLIER = Types.SUPPLIER + "_" + ID;
            internal const string USER = Types.USER + "_" + ID;
        }

        internal static class Result
        {
            internal const string GENERAL = "general";
            internal const string MESSAGES = "messages";
            internal const string PAYLOAD = "payload";
            internal const string SUCCESS = "success";
            internal const string ERROR = "error";
        }

        internal static class Count
        {
            internal const string ACCESSORIES = "accessories_count";
            internal const string ASSETS = "assets_count";
            internal const string ASSIGNED_ASSETS = "assigned_assets_count";
            internal const string COMPONENTS = "components_count";
            internal const string CONSUMABLES = "consumables_count";
            internal const string ITEM = "item_count";
            internal const string LICENSES = "licenses_count";
            internal const string USERS = "users_count";
            internal const string CHECKIN = "checkin_counter";
            internal const string CHECKOUT = "checkout_counter";
            internal const string REQUESTS = "requests_counter";
        }

        internal static class Accessory
        {
            internal const string MINIMUM_QUANTITY = "min_qty";
            internal const string MINIMUM_AMOUNT = "min_amt";
            internal const string REMAINING_QUANTITY = "remaining_qty";
        }

        internal static class Asset
        {
            internal const string END_OF_LIFE = "eol";
            internal const string ORDER_NUMBER = "order_number";
            internal const string RTD_LOCATION = "rtd_" + Types.LOCATION;
            internal const string WARRANTY_MONTHS = "warranty_months";
            internal const string WARRANTY_EXPIRES = "warranty_expires";
            internal const string USER_CAN_CHECKOUT = "user_can_checkout";
            internal const string LAST_AUDIT = "last_audit_date";
            internal const string NEXT_AUDIT = "next_audit_date";
            internal const string PURCHASE_DATE = "purchase_date";
            internal const string LAST_CHECKOUT = "last_checkout";
            internal const string PURCHASE_COST = "purchase_cost";
            internal const string STATUS_TYPE = "status_type";
            internal const string STATUS_META = "status_meta";
        }

        internal static class Category
        {
            internal const string CATEGORY_TYPE = "category_type";
            internal const string CHECKIN_EMAIL = "checkin_email";
            internal const string EULA = "eula";
            internal const string EULA_TEXT = "eula_text";
            internal const string HAS_EULA = "has_eula";
            internal const string REQUIRE_ACCEPTANCE = "require_acceptance";
            internal const string USE_DEFAULT_EULA = "use_default_eula";
        }

        internal static class Consumable
        {
            internal const string ITEM_NO = "item_no";
            internal const string REMAINING = "remaining";
        }

        internal static class CustomField
        {
            internal const string DB_COLUMN_NAME = "db_column_name";
            internal const string FIELD_VALUES = "field_values";
            internal const string FIELD_VALUES_ARRAY = "field_values_array";
            internal const string FORMAT = "format";
            internal const string REQUIRED = "required";
        }

        internal static class Depreciation
        {
            internal const string MONTHS = "months";
        }

        internal static class Fieldset
        {
            internal const string FIELDS = "fields";
            internal const string MODELS = "models";
        }

        internal static class License
        {
            internal const string EXPIRATION_DATE = "expiration_date";
            internal const string FREE_SEATS_COUNT = "free_seats_count";
            internal const string LICENSE_EMAIL = "license_email";
            internal const string LICENSE_NAME = "license_name";
            internal const string MAINTAINED = "maintained";
            internal const string PRODUCT_KEY = "product_key";
            internal const string PURCHASE_ORDER = "purchase_order";
            internal const string SEATS = "seats";
            internal const string TERMINATION_DATE = "termination_date";
        }

        internal static class LicenseSeat
        {
            internal const string ASSIGNED_ASSET = "assigned_asset";
            internal const string ASSIGNED_USER = "assigned_user";
            internal const string REASSIGNABLE = "reassignable";
        }

        internal static class Location
        {
            internal const string ADDRESS = "address";
            internal const string ADDRESS2 = "address2";
            internal const string CITY = "city";
            internal const string STATE = "state";
            internal const string COUNTRY = "country";
            internal const string ZIP = "zip";
            internal const string PARENT = "parent";
            internal const string CHILDREN = "children";
            internal const string LDAP_OU = "ldap_ou";
        }

        internal static class LoginAttempt
        {
            internal const string USER_AGENT = "user_agent";
            internal const string REMOTE_IP = "remote_ip";
            internal const string SUCCESSFUL = "successful";
        }

        internal static class Maintenance
        {
            internal const string ASSET_MAINTENANCE_TIME = "asset_maintenance_time";
            internal const string ASSET_MAINTENANCE_TYPE = "asset_maintenance_type";
            internal const string COMPLETION_DATE = "completion_date";
            internal const string IS_WARRANTY = "is_warranty";
            internal const string START_DATE = "start_date";
        }

        internal static class Manufacturer
        {
            internal const string SUPPORT_EMAIL = "support_email";
            internal const string SUPPORT_PHONE = "support_phone";
            internal const string SUPPORT_URL = "support_url";
        }

        internal static class Error
        {
            internal const string VALUE_EMPTY = "Value cannot be null or empty.";
            internal const string NULL_DESERIALIZING_STRING = "Encountered null while deserializing string.";
            internal const string NULL_DESERIALIZING_DICT = "Encountered null while deserializing dictionary.";
            internal const string UNKNOWN_MESSAGES_TYPE = "Unkown JsonElement value kind for \"" + Result.MESSAGES + "\" key.";
        }

        internal static class Actions
        {
            internal const string CANCEL = "cancel";
            internal const string CHECKIN = "checkin";
            internal const string CHECKOUT = "checkout";
            internal const string CLONE = "clone";
            internal const string DELETE = "delete";
            internal const string REQUEST = "request";
            internal const string RESTORE = "restore";
            internal const string UPDATE = "update";
        }

        internal static class Request
        {
            internal const string REQUEST_DATE = "request_date";
        }

        internal static class StatusLabel
        {
            internal const string COLOR = "color";
            internal const string DEFAULT_LABEL = "default_label";
            internal const string SHOW_IN_NAV = "show_in_nav";
        }

        internal static class Supplier
        {
            internal const string CONTACT = "contact";
            internal const string FAX = "fax";
        }

        internal static class User
        {
            internal const string ACTIVATED = "activated";
            internal const string AVATAR = "avatar";
            internal const string EMAIL = "email";
            internal const string EMPLOYEE_NUMBER = "employee_num";
            internal const string FIRST_NAME = "first_name";
            internal const string LAST_LOGIN = "last_login";
            internal const string LAST_NAME = "last_name";
            internal const string LDAP_IMPORT = "ldap_import";
            internal const string PHONE = "phone";
            internal const string TITLE = "jobtitle";
            internal const string TWO_FACTOR_ACTIVATED = "two_factor_activated";
            internal const string TWO_FACTOR_ENROLLED = "two_factor_enrolled";
            internal const string WEBSITE = "website";
        }
    }
}
