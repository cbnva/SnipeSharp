# SnipeIT Bugs
- GET /api/v1/statuslabels/N/deployable (where N is a valid ID) appends debug assets to the return message when API_DEBUG=true
- GET /api/v1/manufacturers allows sorting on components_count but not accessories_count (accessories_count is retrieved, components_count is not).
- Can't get the is_warranty field of AssetMaintenances.
- Accessories can be sorted on `min_amt`, but the field is named `min_qty`.
- Accessories can be sorted on `eol`, which is not a field they have.
- Can get but not set `notes` on `Accessory` (even through the web interface).
- Can set but not get `requestable` on `Accessory`.
