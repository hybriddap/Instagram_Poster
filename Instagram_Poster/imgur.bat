:: Set the local path to the image you want to upload
set IMAGE_PATH=%1
echo %IMAGE_PATH%
:: Set the Imgur Client-ID (replace with your actual Client-ID)
set CLIENT_ID=dfc65d08059003e

:: Set the API URL
set API_URL=https://api.imgur.com/3/image

:: Upload image to Imgur using curl
curl --location %API_URL% ^
--header "Authorization: Client-ID %CLIENT_ID%" ^
--form "image=@%IMAGE_PATH%" ^
--form "type=image" ^
--form "title=Simple upload" ^
--form "description=This is a simple image upload in Imgur"