package com.example.tenisklubapp;

import androidx.appcompat.app.AppCompatActivity;



import android.os.Bundle;

import android.content.Intent;
import android.os.Bundle;
import android.text.method.ScrollingMovementMethod;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonArrayRequest;
import com.android.volley.toolbox.Volley;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;
import java.util.ArrayList;




public class MainActivity extends AppCompatActivity {


    private RequestQueue requestQueue;
    private TextView osebe;
    private String url = "https://tenisklub-is-dev.azurewebsites.net/api/v1/Igralec\n";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        requestQueue = Volley.newRequestQueue(getApplicationContext());
        osebe = (TextView) findViewById(R.id.osebe);
    }

    public  void prikaziOsebe(View view){
        if (view != null){
            JsonArrayRequest request = new JsonArrayRequest(url, jsonArrayListener, errorListener);
            requestQueue.add(request);
        }
    }


    private Response.Listener<JSONArray> jsonArrayListener = new Response.Listener<JSONArray>() {
        @Override
        public void onResponse(JSONArray response){
            ArrayList<String> data = new ArrayList<>();

            for (int i = 0; i < response.length(); i++){
                try {
                    JSONObject object =response.getJSONObject(i);
                    String name = object.getString("ime");
                    String surname = object.getString("priimek");


                    data.add(name + " " + surname);

                } catch (JSONException e){
                    e.printStackTrace();
                    return;

                }
            }

            osebe.setText("");


            for (String row: data){
                String currentText = osebe.getText().toString();
                osebe.setText(currentText + "\n\n" + row);
            }

        }

    };

    private Response.ErrorListener errorListener = new Response.ErrorListener() {
        @Override
        public void onErrorResponse(VolleyError error) {
            Log.d("REST error", error.getMessage());
        }
    };


    public static final String EXTRA_MESSAGE = "com.example.TenisKlubApp.MESSAGE";

    public void addIgralecActivity (View view) {
        Intent intent = new Intent(this,AddIgralecActivity.class);
        String message = "Dodaj Igralca v seznam.";
        intent.putExtra(EXTRA_MESSAGE, message);
        startActivity(intent);
    }

}