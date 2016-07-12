from django.shortcuts import render
from django.shortcuts import redirect
# Create your views here.
from allauth.socialaccount.models import SocialAccount
from userlist.models import Ban_user
from django.contrib.auth.models import User

def showlist(request):
    whoami = request.user.username
    allbanned = Ban_user.objects.all()
    bannedarr = []
    for user in allbanned:
        bannedarr.append(user.name)
    if whoami == '':
        return render(request, "err.html")
    elif whoami in bannedarr:
        return render(request, "banned.html")
    allusers = SocialAccount.objects.all()
    return render(request, "list.html", {"users": allusers, 'whoami': whoami})


def login(request):
    return redirect("/accounts/login/")


def deleteuser(request):
    uid = request.GET['uid']
    whoami = request.GET['whoami']
    for item in SocialAccount.objects.all():
        if item.user.username == uid:
            item.delete()
    if whoami == '' or whoami==uid:
        return render(request, "err.html")
    return redirect("/userlist/")


def banuser(request):
    b = Ban_user(name=request.GET['whoami'])
    b.save()
    # print(request.GET['uid'])
    return redirect("/userlist/")

def unbanuser(request):
    try:
        print("kek")
        b = Ban_user.objects.get(name=request.GET['whoami'])
        b.delete()
    except:
        return redirect("/userlist/")
    # print(request.GET['uid'])
    return redirect("/userlist/")
