from django.db import models

# Create your models here.

class User(models.Model):
    id = models.CharField(max_length=100)
    name = models.CharField(max_length=100, primary_key=True)
    socialnetwork = models.CharField(max_length=100)
    isbanned = models.BooleanField(default=False)
    issuperuser = models.BooleanField(default=False)

