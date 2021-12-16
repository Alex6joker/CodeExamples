package com.example.GuessTheNumber;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.TextView;

public class HelpActivity extends AppCompatActivity {
    public static final String EXTRA_INDEX_OF_TARGET =
            "com.example.GuessTheNumber.index_of_target";
    int indexOfTarget;

    //Массив указателей на ресурсы с текстом подсказок
    //Обратите внимание, что он целочисленный!
    //Помним, что указатели на ресурсы имеют тип int
    private int[] mHelpArray = {
            R.string.help_target_text_1,
            R.string.help_target_text_2,
            R.string.help_target_text_3
    };
    private TextView helpText;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_help);
        indexOfTarget = getIntent().getIntExtra(EXTRA_INDEX_OF_TARGET, 0);
        helpText = (TextView)findViewById(R.id.helpTextView);
        helpText.setText(mHelpArray[indexOfTarget]);
    }
}
