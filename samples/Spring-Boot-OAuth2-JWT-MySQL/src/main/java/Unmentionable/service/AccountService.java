package Unmentionable.service;

import Unmentionable.model.Account;

import java.util.Collection;

/**
 * Created by christospapidas on 24012016--.
 */
public interface AccountService {

    Collection<Account> findAll();

    Account findByUsername(String userename);

    Account createNewAccount(Account account);


}
