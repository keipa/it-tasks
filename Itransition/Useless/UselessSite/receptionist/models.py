from django.db import models


class AdditionalUserData(models.Model):
    uid = models.CharField(max_length=255)
    is_banned = models.BooleanField(default=False)
