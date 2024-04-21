import os

def create_directories(input_file):
    with open(input_file, 'r') as file:
        lines = file.readlines()
    counter = [0,0,0,0,0,0]
    for line in lines:
        parts = line.strip().split('\t')
        text = parts[0]
        class_name = 'class_' + parts[1]
        file_name = class_name + '/{}_{}.txt'.format(class_name[6:], counter[int(parts[1]) - 1])
        counter[int(parts[1]) - 1] += 1

        if not os.path.exists(class_name):
            os.makedirs(class_name)

        with open(file_name, 'w') as output_file:
            output_file.write(text)

if __name__ == "__main__":
    input_file = input("Enter the path to the input file: ")
    create_directories(input_file)
    print("Directories and files created successfully.")
