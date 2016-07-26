import sys
s,l,r,d='',len,range,sys.argv[1:]
if l(d)>1 and l(d[0])>0:
 for i in r(l(d[0])):
  for j in r(l(d[0])-i+1):
   if j>l(s) and all(d[0][i:i+j] in x for x in d):
    s=d[0][i:i+j]
print(s)
2