package com.example.GuessTheNumber;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageButton;
import android.widget.TextView;
import android.widget.Toast;
import android.util.Log;

import java.util.Random;

public class MainActivity extends AppCompatActivity {

    Button zeroButton, firstButton, secondButton, thirdButton, devInfo, helpButton;
    ImageButton nextButton, prevButton; //Объект кнопки «Следующий»
    TextView targetTextView, helpIsUsedTextView; //Объект виджета TextView
    int targetIndex;
    boolean[] helpIsUsedArray; // В данной цели уже была открыта подсказка


    //Создание и инициализация массива вопросов
    Questions[] mQuestionArray;

    Toast lastToast;
    static final String KEY_INDEX = "index"; //ключ для Bundle
    static final String KEY_INDEX_HELPS_USING = "indexHelpsUsing"; //ключ для Bundle

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        Log.d(this.getLocalClassName(), "onCreate(Bundle) called");
        Toast lastToast = new Toast(getApplicationContext());
        setContentView(R.layout.activity_main);

        if (savedInstanceState != null) {
            targetIndex = savedInstanceState.getInt(KEY_INDEX, 0);
            helpIsUsedArray = savedInstanceState.getBooleanArray(KEY_INDEX_HELPS_USING);
            if(helpIsUsedArray == null)
                helpIsUsedArray = new boolean[]{
                        false,
                        false,
                        false
                };
        }
        else
        {
            targetIndex = 0;
            helpIsUsedArray = new boolean[]{
                    false,
                    false,
                    false
            };
        }

        mQuestionArray = new Questions[]{
                new Questions(R.string.target_info_text_1, true),
                new Questions(R.string.target_info_text_2, false),
                new Questions(R.string.target_info_text_3, true),
        };

        targetTextView = (TextView) findViewById(R.id.target_info_text);
        int question = mQuestionArray[targetIndex].getTarget();
        targetTextView.setText(question);

        helpIsUsedTextView = (TextView) findViewById(R.id.using_of_help_button);
        ChangeHelpUsingText();

        zeroButton = (Button) findViewById(R.id.zero_button);

        // Назначение слушателя для кнопки «0»
        zeroButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CheckNumber(0);
            }
        });

        firstButton = (Button) findViewById(R.id.first_button);

        // Назначение слушателя для кнопки «1»
        firstButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CheckNumber(1);
            }
        });

        secondButton = (Button) findViewById(R.id.second_button);

        // Назначение слушателя для кнопки «2»
        secondButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CheckNumber(2);
            }
        });

        thirdButton = (Button) findViewById(R.id.third_button);

        // Назначение слушателя для кнопки «3»
        thirdButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                CheckNumber(3);
            }
        });

        devInfo = (Button) findViewById(R.id.dev_info);

        // Назначение слушателя для кнопки «Информация о разработчиках»
        devInfo.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                MakeAndShowNewToast(R.string.dev_info_text);
            }
        });

        helpButton = (Button) findViewById(R.id.help_button);
        helpButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent i = new Intent(MainActivity.this, HelpActivity.class);
                i.putExtra(HelpActivity.EXTRA_INDEX_OF_TARGET, targetIndex);
                startActivityForResult(i, 0);
            }
        });

        nextButton = (ImageButton) findViewById(R.id.next_button);
        nextButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                targetIndex = (targetIndex + 1) % mQuestionArray.length;
                UpdateQuestion();
                ChangeHelpUsingText();
            }
        });

        prevButton = (ImageButton) findViewById(R.id.prev_button);
        prevButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (targetIndex > 0)
                    targetIndex = (targetIndex - 1) % mQuestionArray.length;
                else
                    targetIndex = mQuestionArray.length - 1;
                UpdateQuestion();
                ChangeHelpUsingText();
            }
        });
    }

    private int MakeRandomNumber() {
        return (int) (int) (Math.random() * ((3 - 0) + 1));
    }

    private void CheckNumber(int checkedNumber) {
        if (targetIndex == 1)
            if (checkedNumber == 1) {
                MakeAndShowNewToast(R.string.right_toast);
            } else {
                MakeAndShowNewToast(R.string.wrong_toast);
            }
        else if (targetIndex == 2)
            if (checkedNumber == 3) {
                MakeAndShowNewToast(R.string.right_toast);
            } else {
                MakeAndShowNewToast(R.string.wrong_toast);
            }
        else if (MakeRandomNumber() == checkedNumber) {
            MakeAndShowNewToast(R.string.right_toast);
        } else {
            MakeAndShowNewToast(R.string.wrong_toast);
        }
    }

    void MakeAndShowNewToast(int toastTextID) {
        MakeNewToast(toastTextID);
        ShowNewToast();
    }

    void MakeNewToast(int toastTextID) {
        if (lastToast != null) {
            lastToast.cancel();
        }
        lastToast = Toast.makeText(MainActivity.this,
                toastTextID,
                Toast.LENGTH_SHORT);
    }

    void ShowNewToast() {
        lastToast.show();
    }

    private void UpdateQuestion() {
        //Обновление текста вопроса
        int question = mQuestionArray[targetIndex].getTarget();
        targetTextView.setText(question);
    }

    @Override
    public void onStart() {
        super.onStart(); //Сначала вызывается метод суперкласса…
        Log.d(this.getLocalClassName(), "onStart() called"); //и только потом какое-либо
        //пользовательское добавление
    }

    @Override
    public void onPause() {
        super.onPause();
        Log.d(this.getLocalClassName(), "onPause() called");
    }

    @Override
    public void onResume() {
        super.onResume();
        Log.d(this.getLocalClassName(), "onResume() called");
    }

    @Override
    public void onStop() {
        super.onStop();
        Log.d(this.getLocalClassName(), "onStop() called");
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        Log.d(this.getLocalClassName(), "onDestroy() called");
    }

    // Переопределяем onSaveInstanceState(Bundle) и добавляем
    // соответствующую запись в журнал
    @Override
    public void onSaveInstanceState(Bundle savedInstanceState) {
        super.onSaveInstanceState(savedInstanceState);
        Log.i(this.getLocalClassName(), "onSaveInstanceState"); //запись в журнал
        savedInstanceState.putInt(KEY_INDEX, targetIndex); //сохранить пару
        savedInstanceState.putBooleanArray(KEY_INDEX_HELPS_USING, helpIsUsedArray);
        // «ключ-значение»
        // в объекте
        // saveInstanceState
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        UpdateHelpUsingText();
    }

    void UpdateHelpUsingText(){
        if(!helpIsUsedArray[targetIndex])
        {
            helpIsUsedArray[targetIndex] = true;
            helpIsUsedTextView.setText(R.string.help_is_used);
        }
        else
            helpIsUsedTextView.setText(R.string.help_is_used);
    }

    void ChangeHelpUsingText(){
        if(!helpIsUsedArray[targetIndex])
            helpIsUsedTextView.setText(R.string.help_never_used);
        else
            helpIsUsedTextView.setText(R.string.help_is_used);
    }
}
