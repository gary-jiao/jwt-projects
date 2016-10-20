package Unmentionable.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

/**
 * Starter controller loads the index.jsp
 * to start the application correctly
 */
@Controller
public class StarterController {

    @RequestMapping("/")
    public String loadIndex(){
        return "index";
    }

}
