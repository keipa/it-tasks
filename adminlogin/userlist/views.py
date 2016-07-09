from django.shortcuts import render
from django.http import HttpResponse
from .models import User


def choose_register(request):
    return render(request, 'register.html')