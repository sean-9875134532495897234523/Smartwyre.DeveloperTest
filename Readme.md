# Notes

I spent a lot longer on this in the end, mostly because I decided to focus on the architecture.

The solution is over-engineered for what it is, but I was trying to show how I would approach the architecture if this was a real world application that I was going to continue to develop in the future.

I made the fake db async to show that if you had a real db that had an async api then you would use that.

I added this initial data so that you can use the console application:

| Rebate identifier      | Incentive         |
| ---------------------- | ----------------- |
| rebate-001             | FixedCashAmount   |
| rebate-002             | FixedRateRebate   |
| rebate-003             | AmountPerUom      |

| Product identifier      | Supported incentives  |
| ----------------------- | --------------------- |
| product-001             | FixedCashAmount       |
| product-002             | FixedRateRebate       |
| product-003             | AmountPerUom          |
