import tensorflow as tf
from tensorflow.keras import Sequential
from tensorflow.keras.layers import Dense, Input, Flatten, Conv2D, MaxPool2D, TextVectorization, Embedding, Dropout, GlobalAveragePooling1D
from tensorflow.keras import losses
from keras import optimizers
import numpy as np
import os

questionsArr = []
gradesArr = []

dataset_dir = 'model\datasets\sorted'

batch_size = 100
seed = 42

raw_train_ds = tf.keras.utils.text_dataset_from_directory(
    dataset_dir,
    batch_size=batch_size,
    validation_split=0.2,
    subset='training',
    seed=seed)

raw_val_ds = tf.keras.utils.text_dataset_from_directory(
    dataset_dir,
    batch_size=batch_size,
    validation_split=0.2,
    subset='validation',
    seed=seed)

raw_test_ds = tf.keras.utils.text_dataset_from_directory(
    dataset_dir,
    batch_size=batch_size)

max_features = 10000
sequence_length = 250

vectorize_layer = TextVectorization(
    standardize='lower',
    split='whitespace',
    max_tokens=max_features,
    output_mode='int',
    output_sequence_length=sequence_length)

train_text = raw_train_ds.map(lambda x, y: x)
vectorize_layer.adapt(train_text)

def vectorize_text(text, label):
  text = tf.expand_dims(text, -1)
  return vectorize_layer(text), label


text_batch, label_batch = next(iter(raw_train_ds))
first_review, first_label = text_batch[0], label_batch[0]
print("Review", first_review)
print("Label", raw_train_ds.class_names[first_label])
print("Vectorized review", vectorize_text(first_review, first_label))

print("2 ---> ",vectorize_layer.get_vocabulary()[2])
print(" 2709 ---> ",vectorize_layer.get_vocabulary()[2709])
print('Vocabulary size: {}'.format(len(vectorize_layer.get_vocabulary())))

train_ds = raw_train_ds.map(vectorize_text)
val_ds = raw_val_ds.map(vectorize_text)
test_ds = raw_test_ds.map(vectorize_text)

AUTOTUNE = tf.data.AUTOTUNE

train_ds = train_ds.cache().prefetch(buffer_size=AUTOTUNE)
val_ds = val_ds.cache().prefetch(buffer_size=AUTOTUNE)
test_ds = test_ds.cache().prefetch(buffer_size=AUTOTUNE)

embedding_dim = 16

model = tf.keras.Sequential([
  Embedding(max_features, embedding_dim),
  Dropout(0.2),
  GlobalAveragePooling1D(),
  Dropout(0.2),
  Dense(1, activation='sigmoid')])

model.summary()

model.compile(loss=losses.BinaryCrossentropy(),
              optimizer='adam',
              metrics=[tf.metrics.BinaryAccuracy(threshold=0.5)])

epochs = 10
history = model.fit(
    train_ds,
    validation_data=val_ds,
    epochs=epochs)

loss, accuracy = model.evaluate(test_ds)

print("Loss: ", loss)
print("Accuracy: ", accuracy)
# cutoff = int(len(questionsArr) * 0.8)

# model = Sequential([
#           Input(shape=(32,32,3,)),
#           Conv2D(filters=6, kernel_size=(5,5), padding="same", activation="relu"),
#           MaxPool2D(pool_size=(2,2)),
#           Conv2D(filters=16, kernel_size=(5,5), padding="same", activation="relu"),
#           MaxPool2D(pool_size=(2, 2)),
#           Conv2D(filters=120, kernel_size=(5,5), padding="same", activation="relu"),
#           Flatten(),
#           Dense(units=84, activation="relu"),
#           Dense(units=10, activation="softmax"),
#       ])
 
# trainX = np.array(questionsArr[:cutoff])
# trainY = np.array(gradesArr[:cutoff])
# testX = np.array(questionsArr[cutoff:])
# testY = np.array(gradesArr[cutoff:])
# print(trainX);
# model.compile(optimizer="adam", loss=tf.keras.losses.SparseCategoricalCrossentropy(), metrics=["acc"])


# history = model.fit(x=trainX, y=trainY, batch_size=256, epochs=10, validation_data=(testX, testY))
