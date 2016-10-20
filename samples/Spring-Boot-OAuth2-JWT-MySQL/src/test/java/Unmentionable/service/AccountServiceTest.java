package Unmentionable.service;

import Unmentionable.AbstractTest;
import Unmentionable.model.Account;
import Unmentionable.model.Role;
import org.junit.After;
import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.test.context.junit4.SpringJUnit4ClassRunner;
import org.springframework.transaction.annotation.Transactional;

import javax.persistence.EntityExistsException;
import javax.persistence.NoResultException;
import javax.validation.ConstraintViolationException;
import java.util.Collection;
import java.util.HashSet;
import java.util.Set;

/**
 * Tests for the account service
 */
@Transactional
@RunWith(SpringJUnit4ClassRunner.class)
public class AccountServiceTest extends AbstractTest {

    @Autowired
    private AccountService accountService;


    @Before
    public void setUp(){
        // Nothing yet
    }

    @After
    public void tearDown(){
        // Nothing yet
    }

    /**
     * Get all accounts from database
     * - The list should not be null
     */
    @Test
    public void testGetAllAccounts(){

        // Add 2 accounts
        createNewAccount("account1", "account111");
        createNewAccount("account2", "account222");

        // Get all accounts from database
        Collection<Account> accountCollection = accountService.findAll();

        // Accounts should be null
        Assert.assertNotNull("failure - Accounts collection should not be null");
    }

    /**
     * Test the account registration
     *  - if after creation the object is null
     *  - if after creation the id is null
     *  - if after creation the size of table has increment by 1 row
     */
    @Test
    public void testRegister(){

        // Get all accounts
        Collection<Account> accountCollection = accountService.findAll();

        // Create a new account into database and get the object back
        Account createdAccount = createNewAccount("SampleUsername", "123123123Password");

        // Get user roles
        Set<Role> roles =  createdAccount.getRoles();

        // Object and id of the object should not be null
        Assert.assertNotNull("failure - the returned object should not be null", createdAccount);
        Assert.assertNotNull("failure - the id of returned object should not be null", createdAccount.getId());


        // Role size should be equals to 1 and not be null also be ROLE_USER
        Assert.assertNotNull("failure - should not be null", roles.iterator().next());
        Assert.assertEquals("failure - should have a role of simple user", roles.size(), 1);
        Assert.assertEquals("failure - should have a role of ROLE_USER as code", roles.iterator().next().getCode(), "ROLE_USER");

        // Get all accounts
        Collection<Account> accountCollectionAfterCreation = accountService.findAll();

        // Check if the table count is grater + 1
        Assert.assertEquals("failure - the created account is not in database", accountCollectionAfterCreation.size() - accountCollection.size(), 1);

    }

    /**
     * Check what happens when a user try to put the same username with someone else
     * - Should throw an exception
     * - The exception should be type of DataIntegrityViolationException
     * - The second createdAccount2 should be null (not created)
     * - The first account should be created
     */
    @Test
    public void testRegisterSameUsername(){
        // Create a new account into database and get the object back
        Account createdAccount1 = createNewAccount("SampleUsernameSame", "123123123Password");
        Account createdAccount2 = null;

        Exception e = null;

        try{
            // Create a new account into database and get the object back
            createdAccount2 = createNewAccount("SampleUsernameSame", "ThisIsAnotherPassword");
        }catch (DataIntegrityViolationException nre){
            e = nre;
        }

        // Check if the exception is null and if is instanceof DataIntegrityViolationException
        Assert.assertNotNull("failure - expect exception not be null ", e);
        Assert.assertTrue("failure - the username should be unique", e instanceof DataIntegrityViolationException);

        // The second account should not be created
        Assert.assertNull("The first account should NOT be created ", createdAccount2);

        // The first account should be successful created
        Assert.assertNotNull("failure - the first account should be created successfully", createdAccount1);
    }

    /**
     * Check if user try to create an account with invalid username
     * - Check the exception if null
     * - Check the exception if is type of ConstraintViolationException
     * - Check if the account created
     */
    @Test
    public void testInvalidUsername(){

        Account createdAccount = null;
        Exception e = null;

        try{
            // Create a new account with invalid username
            createdAccount = createNewAccount("123456", "123123123Password");
        }catch (ConstraintViolationException nre){
            e = nre;
        }

        // Check if the exception is null and if is instanceof DataIntegrityViolationException
        Assert.assertNotNull("failure - expect exception not be null ", e);
        Assert.assertTrue("failure - the username should be unique", e instanceof ConstraintViolationException);

        // The account should be null and NOT created
        Assert.assertNull("The first account should NOT be created ", createdAccount);
    }

    /**
     * Check if user try to create an account with invalid password
     * - Check the exception if null
     * - Check the exception if is type of ConstraintViolationException
     * - Check if the account created
     */
    @Test
    public void testInvalidPassword(){

        Account createdAccount = null;
        Exception e = null;

        try{
            // Create a new account with invalid username
            createdAccount = createNewAccount("123456999", "12345");
        }catch (EntityExistsException nre){
            e = nre;
        }

        // Check if the exception is null and if is instanceof EntityExistsException
        Assert.assertNotNull("failure - expect exception not be null ", e);
        Assert.assertTrue("failure - the username should be unique", e instanceof EntityExistsException);

        // The account should be null and NOT created
        Assert.assertNull("The first account should NOT be created ", createdAccount);
    }


    /**
     * Create a new account into database
     * @param username - string of user username
     * @param password - the account password
     * @return the created account object
     */
    private Account createNewAccount(String username, String password){

        // Create an account object
        Account account = new Account();
        account.setUsername(username);
        account.setPassword(password);

        // Create a new account
        return accountService.createNewAccount(account);
    }


}
