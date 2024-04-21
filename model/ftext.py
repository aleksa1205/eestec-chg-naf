import fasttext

file=open("D:\\eestec-chg-naf\\ftext.txt","r+")
ptr=file.readline()
print(ptr)
arr=fasttext.get_nearest_neighbors(ptr, 5)
print(arr)
for x in arr:
    print(x)
    file.write(x[1]+'\n')
file.close()
