from django.http import HttpResponse
from django.views.generic import View
from django.shortcuts import render_to_response
from django.template.context import RequestContext


class HomeView(View):
    template_name = 'home.djhtml'

    def get(self, request, *args, **kwargs):
        context = RequestContext(request, {'request': request,
                                           'user': request.user})
        return render_to_response(self.template_name,
                                  context_instance=context)

    def post(self, request, *args, **kwargs):
        return HttpResponse('Useless response')
