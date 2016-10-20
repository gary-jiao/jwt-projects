package Unmentionable.controller;

import Unmentionable.AbstractControllerTest;
import Unmentionable.model.Account;
import Unmentionable.service.AccountService;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpHeaders;
import org.springframework.http.MediaType;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.test.web.servlet.MvcResult;
import org.springframework.test.web.servlet.request.MockMvcRequestBuilders;
import org.springframework.transaction.annotation.Transactional;

/**
 * Test the authentication controller
 */
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class AuthenticationControllerTest extends AbstractControllerTest {

    @Autowired
    private AccountService accountService;

    @Before
    public void setUp() {
        super.setUp();
    }

    @Test
    public void testRegister() throws Exception {

        // Url of the request
        String uri = "/register";

        // Create the params
        Account account = new Account();
        account.setUsername("fofoFaasfdasdf234");
        account.setPassword("asdfasdfasdf123123");
        String inputJson = super.mapToJson(account);

        // Create the request
        MvcResult mvcResult = mvc.perform(
                MockMvcRequestBuilders.post(uri).contentType(MediaType.APPLICATION_JSON).accept(MediaType.APPLICATION_JSON).content(inputJson)
        ).andReturn();

        // Collect the data
        String content = mvcResult.getResponse().getContentAsString();
        int status = mvcResult.getResponse().getStatus();

        Assert.assertEquals("failure - expected HTTP status 201", 201, status);
        Assert.assertTrue("failure - expected Http response body to have a value", content.trim().length() > 0);
    }

    @Test
    public void testSample() throws Exception {

        String uri = "/api/sample";

        HttpHeaders httpHeaders = new HttpHeaders();
        httpHeaders.add("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOlsicmVzdHNlcnZpY2UiXSwidXNlcl9uYW1lIjoicGFwaWRha29zIiwic2NvcGUiOlsicmVhZCIsIndyaXRlIl0sImV4cCI6MTQ1NjcxODIwNSwiYXV0aG9yaXRpZXMiOlsiUk9MRV9VU0VSIl0sImp0aSI6IjQ1NjczY2Y0LWYwNDYtNGQxZi04NGE5LTA1NjVmZjhhZThiNyIsImNsaWVudF9pZCI6ImNsaWVudGFwcCJ9.Au9dJencn68uCSoy7w8CrTCLoT2KTAB8gDKYrww9KxA");

        MvcResult mvcResult = mvc.perform(
          MockMvcRequestBuilders.post(uri).headers(httpHeaders).contentType(MediaType.APPLICATION_JSON).accept(MediaType.APPLICATION_JSON).content("")
        ).andReturn();

        // Collect the data
        String content = mvcResult.getResponse().getContentAsString();
        int status = mvcResult.getResponse().getStatus();

        Assert.assertEquals("failure - expected HTTP status 201", 201, status);
        Assert.assertTrue("failure - expected Http response body to have a value", content.trim().length() > 0);

    }

}
