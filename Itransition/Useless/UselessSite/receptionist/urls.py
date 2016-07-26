from django.conf.urls import include, url
from django.conf import settings
from django.conf.urls.static import static
from receptionist.views import HomeView

urlpatterns = [
    url(r'^$', HomeView.as_view(), name='home_view'),
    url('', include('social.apps.django_app.urls', namespace='social')),
    url('', include('django.contrib.auth.urls', namespace='auth')),
] + static(settings.STATIC_URL, document_root=settings.STATIC_ROOT)
