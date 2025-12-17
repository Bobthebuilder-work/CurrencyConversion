# CurrencyConversion

# Assumptions

# Authentication & Roles
   The system uses JWT (JSON Web Tokens) for secure communication. 
   Tokens are valid for 60 minutes.
### Credentials
* Admin admin admin123
* User user user123

### Role-Based Access Control
*  Admin: Full access to all endpoints, including HistoricRates.
* User: Access to ConversionRate and ExchangeRate only.

### Rate Limiting Policies
* To ensure system stability and prevent abuse, two distinct rate-limiting policies are implemented:
## Authentication Policy: 
  * Limit: 10 requests per minute.
  * Scope: Applied per IP Address.
  * Purpose: Protects against brute-force attacks on login endpoints.
## Conversion Policy: 
  * Limit: 100 requests per minute.
  * Scope: Applied per User.
  * Purpose: Ensures fair usage of the conversion engine.
