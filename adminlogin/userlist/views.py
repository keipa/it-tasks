from django.shortcuts import render
from django.http import HttpResponse
from .models import User
import requests
import json
# from __future__ import print_function
from requests_oauthlib import OAuth1Session
import webbrowser
from twitter import *

REQUEST_TOKEN_URL = 'https://api.twitter.com/oauth/request_token'
ACCESS_TOKEN_URL = 'https://api.twitter.com/oauth/access_token'
AUTHORIZATION_URL = 'https://api.twitter.com/oauth/authorize'
SIGNIN_URL = 'https://api.twitter.com/oauth/authenticate'



def choose_register(request):
    return render(request, 'register.html')

def facebook(request):
    url_to_get_token = "https://graph.facebook.com/v2.3/oauth/access_token?%20" \
                       "client_id=1744462099145501" \
                       "&redirect_uri=http://127.0.0.1:8000/login/facebook" \
                       "&client_secret=9235ba83ad20880d69ca8fa2602dce3b&code="
    url_to_get_info = "https://graph.facebook.com/v2.6/me?fields=id%2Cname&access_token="
    code = request.GET["code"]
    r = requests.get(url_to_get_token + code)
    token = json.loads(r.text)['access_token']
    info = requests.get(url_to_get_info + token)
    name = json.loads(info.text)["name"]
    id = json.loads(info.text)["id"]
    print(name)
    p = User(name=name, socialnetwork="Facebook", id=id, isbanned=False, issuperuser=False)
    p.save()
    return render(request, 'register.html')


def vk(request):
    url_to_get_token = "https://oauth.vk.com/access_token?client_id=5542391" \
                       "&client_secret=oTvgZAqdQK0G8Pwouc8q" \
                       "&redirect_uri=http://127.0.0.1:8000/login/vk&code="
    code = request.GET["code"]
    r = requests.get(url_to_get_token + code)
    print(r.text)
    token = json.loads(r.text)['access_token']
    url_to_get_info = "https://api.vk.com/method/users.get?access_token="
    info = requests.get(url_to_get_info + token)
    print(info.text)
    o = json.loads(info.text)
    name = o["response"][0]['first_name']+" "+o["response"][0]['last_name']
    id = str(o["response"][0]["uid"])
    p = User(name=name, socialnetwork="Vk", id=id, isbanned=False, issuperuser=False)
    p.save()
    return render(request, 'register.html')

def twitter(request):
    consumer_key = "k3HSbkazpchiMmUECuTD1TAyq"
    consumer_secret = "X1ZoEdGGTnVYVZYv23IbeUB5xnXPnIxP1ouw1W2oyF7oHvuwOw"

    oauth_client = OAuth1Session(consumer_key, client_secret=consumer_secret, callback_uri='oob')

    print('\nRequesting temp token from Twitter...\n')

    try:
        resp = oauth_client.fetch_request_token(REQUEST_TOKEN_URL)
    except ValueError as e:
        raise 'Invalid response from Twitter requesting temp token: {0}'.format(e)

    url = oauth_client.authorization_url(AUTHORIZATION_URL)

    print('I will try to start a browser to visit the following Twitter page '
          'if a browser will not start, copy the URL to your browser '
          'and retrieve the pincode to be used '
          'in the next step to obtaining an Authentication Token: \n'
          '\n\t{0}'.format(url))

    webbrowser.open(url)
    # pincode = input('\nEnter your pincode? ')

    print('\nGenerating and signing request for an access token...\n')


    return render(request, 'twitter_code.html', {'oauth_token':resp.get('oauth_token'), 'oauth_token_secret':resp.get('oauth_token_secret')})

def twitter_code(request):
    consumer_key = "k3HSbkazpchiMmUECuTD1TAyq"
    consumer_secret = "X1ZoEdGGTnVYVZYv23IbeUB5xnXPnIxP1ouw1W2oyF7oHvuwOw"
    pincode = request.GET['code']
    oauth_client = OAuth1Session(consumer_key, client_secret=consumer_secret,
                                 resource_owner_key=request.GET['oauth_token'],
                                 resource_owner_secret=request.GET['oauth_token_secret'],
                                 verifier=pincode)
    try:
        resp = oauth_client.fetch_access_token(ACCESS_TOKEN_URL)
    except ValueError as e:
        raise 'Invalid response from Twitter requesting temp token: {0}'.format(e)

    print('''Your tokens/keys are as follows:
            consumer_key         = {ck}
            consumer_secret      = {cs}
            access_token_key     = {atk}
            access_token_secret  = {ats}'''.format(
        ck=consumer_key,
        cs=consumer_secret,
        atk=resp.get('oauth_token'),
        ats=resp.get('oauth_token_secret')))
    t = Twitter(
        auth=OAuth(resp.get('oauth_token'),
                   resp.get('oauth_token_secret'),
                   "k3HSbkazpchiMmUECuTD1TAyq", "X1ZoEdGGTnVYVZYv23IbeUB5xnXPnIxP1ouw1W2oyF7oHvuwOw"))

    id = t.statuses.user_timeline(count=1)[0]['id_str']
    name = str(t.statuses.user_timeline(count=1)[0]["user"]["name"])
    print(name)
    p = User(name=name, socialnetwork="Twitter", id=id, isbanned=False, issuperuser=False)
    p.save()
    return render(request, "register.html")