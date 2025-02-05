# Instagram_Poster

This is a little prototype I made to allow a user to upload a photo to Instagram with the click of a button.

It uses:
1. Meta's Graph API - To publish to Instagram
2. Imgur's API - To host the image online for Instagram's API to work

Steps to get it working:
1. Provide your Meta Access Token to the environment variables in Visual Studio (with the name of the variable being 'accessToken')(for your Business API App, not Consumer App, created on Meta)
2. Provide your Instagram User ID to the environment variables in Visual Studio (with the name of the variable being 'instagramUserId')
3. Provide your Imgur Client ID to the environment variables in Visual Studio (with the name of the variable being 'imgurClientID')
4. Build the Program
5. Copy the Bat File provided, into the directory with the exe file
6. Enjoy!

Important Notes:
-You must have a developer account with Meta
-You must have a business account on Instagram
-You must have a facebook page linked to that business account (this will help you retrieve your instagram user id)
-You must have an API app running on Meta and it must have the correct permissions
-You must have an account with Imgur & have an API app running with it
