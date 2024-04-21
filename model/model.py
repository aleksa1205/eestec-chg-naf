import tensorflow as tf
from tensorflow.keras import Sequential
from tensorflow.keras.layers import Dense, Input, Flatten, Conv2D, MaxPool2D
from keras import optimizers
import numpy as np
import os

questionsArr = []
gradesArr = []

file = open('model\datasets\CEFR-SP_SCoRE_train.txt')
for i in range(5):
    questionsArr.append(file.readline().split('\t')[0])
    gradesArr.append(file.readline().split('\t')[1])

cutoff = int(len(questionsArr) * 0.8)

model = Sequential([
          Input(shape=(32,32,3,)),
          Conv2D(filters=6, kernel_size=(5,5), padding="same", activation="relu"),
          MaxPool2D(pool_size=(2,2)),
          Conv2D(filters=16, kernel_size=(5,5), padding="same", activation="relu"),
          MaxPool2D(pool_size=(2, 2)),
          Conv2D(filters=120, kernel_size=(5,5), padding="same", activation="relu"),
          Flatten(),
          Dense(units=84, activation="relu"),
          Dense(units=10, activation="softmax"),
      ])
 
trainX = np.array(questionsArr[:cutoff])
trainY = np.array(gradesArr[:cutoff])
testX = np.array(questionsArr[cutoff:])
testY = np.array(gradesArr[cutoff:])

model.compile(optimizer="adam", loss=tf.keras.losses.SparseCategoricalCrossentropy(), metrics=["acc"])
 
history = model.fit(x=trainX, y=trainY, batch_size=256, epochs=10, validation_data=(testX, testY))
