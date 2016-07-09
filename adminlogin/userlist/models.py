from django.db import models

# Create your models here.

class User(models.Model):
    name = models.CharField(max_length=100)
    socialnetwork = models.CharField(max_length=100)
    isbanned = models.BooleanField(default=False)