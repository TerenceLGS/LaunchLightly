# Launch Lightly
### Don't fumble around in the dark when doing bulk edits

Launch darkly is a wonderful feature flagging tool with some impressive implementation.
It's beautiful if you are only doing a handful, but a bit painful, and prone to user error when doing more.
The web UI is designed to be too fancy schmancy for it's own good,
I was expecting the UI to improve over time, and they have given us bulk edit for user IDs, 
but not the values in a clause. The API has improved much better in this time.

I've taken some inspiration from the User ID bulk edit, and I'm putting together this tool to make it easier to bulk edit values in a clause.

## How to use:
You'll need an SDK token, and an API token. You can create these from the launch darkly website.
<br /> Each project and flag has an ID. Use these to lookup the rule definitions.
Then you can pick a clause and add / remove / replace values in bulk.