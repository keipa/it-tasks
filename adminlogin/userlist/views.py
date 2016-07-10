from django.shortcuts import render
from django.http import HttpResponse
from .models import User
import requests
import json


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


    return render(request, 'register.html')